using NucliaDb;
using NucliaDb.Model;
using NucliaDb.Model.Streaming;

namespace blazor_progress_rag_demo.Services;

/// <summary>
/// Encapsulates the streaming response data from NucliaDb ask operations.
/// </summary>
public class StreamingAskResult
{
    /// <summary>
    /// The accumulated text response from the AI.
    /// </summary>
    public string Response { get; set; } = string.Empty;

    /// <summary>
    /// The original resources retrieved from the knowledge base.
    /// </summary>
    public object? OriginalResources { get; set; }

    /// <summary>
    /// The citations used in the response.
    /// </summary>
    public object? Citations { get; set; }
}

/// <summary>
/// Service that wraps the NucliaDbClient to provide search and ask functionality.
/// </summary>
public class NucliaSearchService
{
    private readonly NucliaDbClient _client;
    private readonly NucliaDbClient _chartsClient;
    private readonly NucliaDbClient _verseClient;

    public NucliaSearchService(NucliaDbClient client, NucliaDbClient chartsClient, NucliaDbClient verseClient)
    {
        _client = client;
        _chartsClient = chartsClient;
        _verseClient = verseClient;
    }

    /// <summary>
    /// Sends an ask request to NucliaDb and streams the response.
    /// Each call maintains separate state for response, resources, and citations.
    /// </summary>
    /// <param name="query">The query string to ask</param>
    /// <param name="onUpdate">Callback for real-time updates of the response text</param>
    /// <param name="topK">The number of top results to return (default: 5)</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>A StreamingAskResult containing the response, original resources, and citations</returns>
    public async Task<StreamingAskResult> AskAsync(string query, Action<string>? onUpdate = null, int topK = 5, CancellationToken cancellationToken = default)
    {
        var result = new StreamingAskResult();

        var askRequest = new AskRequest(query)
        {
            TopK = topK
        };

        var streamResponse = _client.Search.AskStreamAsync(askRequest);
        await foreach (var response in streamResponse)
        {
            switch (response.Item)
            {
                case AnswerContent answer:
                    result.Response += answer.Text;
                    onUpdate?.Invoke(result.Response);
                    await Task.Delay(1); // brief yield for UI responsiveness
                    break;
                case RetrievalContent retrieval:
                    result.OriginalResources = retrieval.Results?.Resources as object;
                    break;
                case CitationsContent citations:
                    result.Citations = citations.Citations.Index();
                    break;
            }
        }

        return result;
    }

    /// <summary>
    /// Sends an ask request to the charts knowledge box and streams the response.
    /// </summary>
    public async Task<StreamingAskResult> AskChartsAsync(string query, Action<string>? onUpdate = null, int topK = 5, CancellationToken cancellationToken = default)
    {
        var result = new StreamingAskResult();

        var askRequest = new AskRequest(query)
        {
            TopK = topK,
            AnswerJsonSchema = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(Schemas.ChartJsonSchema),
            Citations = new Citations(false),
        };

        // Try non-streaming to see if it handles the schema response better
        /*
        var streamResponse = _chartsClient.Search.AskStreamAsync(askRequest);
        await foreach (var response in streamResponse)
        {
            switch (response.Item)
            {
                case AnswerContent answer:
                    result.Response += answer.Text;
                    onUpdate?.Invoke(result.Response);
                    await Task.Delay(1); // brief yield for UI responsiveness
                    break;
                case RetrievalContent retrieval:
                    result.OriginalResources = retrieval.Results?.Resources as object;
                    break;
                case CitationsContent citations:
                    result.Citations = citations.Citations.Index();
                    break;
            }
        }
        */

        // Fallback to AskAsync (non-streaming) to debug
        try 
        {
            var response = await _chartsClient.Search.AskAsync(askRequest);
            
            // Use dynamic to inspect the response since we don't know the exact properties of SyncAskResponse
            // and to avoid compile errors if we guess wrong.
            if (response.Data != null)
            {
                dynamic data = response.Data;
                
                // The response JSON shows an "answer_json" property.
                // We need to extract this. Since we are using dynamic, we can try to access it directly
                // or serialize the whole thing and then parse it.
                
                // Based on the console output, the structure is:
                // { "answer": "", "answer_json": { ... }, ... }
                // Note: "answer" is empty string, "answer_json" has the content.
                
                // Let's try to get answer_json directly.
                // If the SDK model doesn't have the property, dynamic might fail if it's a strong typed object without that property.
                // However, if it's a JsonElement or similar, it might work.
                
                // Safest bet: Serialize to JSON string, then extract "answer_json" part.
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                
                // We need to extract the "answer_json" object from this JSON.
                using var doc = System.Text.Json.JsonDocument.Parse(json);
                System.Text.Json.JsonElement answerJsonElement;
                if (doc.RootElement.TryGetProperty("answer_json", out answerJsonElement))
                {
                    // This is the part we want to return as the response string
                    // so the UI can deserialize it into ChartAugmentedAnswer
                    result.Response = answerJsonElement.GetRawText();
                    onUpdate?.Invoke(result.Response);
                }
                else
                {
                    // Fallback if answer_json is missing
                    System.Text.Json.JsonElement answerElement;
                    if (doc.RootElement.TryGetProperty("answer", out answerElement))
                    {
                         result.Response = answerElement.GetString() ?? "";
                         onUpdate?.Invoke(result.Response);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AskAsync failed: {ex.Message}");
            throw;
        }

        return result;
    }

    /// <summary>
    /// Sends an ask request to the verse knowledge box and streams the response.
    /// </summary>
    public async Task<StreamingAskResult> AskVerseAsync(string query, Action<string>? onUpdate = null, int topK = 5, CancellationToken cancellationToken = default)
    {
        var result = new StreamingAskResult();

        var askRequest = new AskRequest(query)
        {
            TopK = topK
        };

        var streamResponse = _verseClient.Search.AskStreamAsync(askRequest);
        await foreach (var response in streamResponse)
        {
            switch (response.Item)
            {
                case AnswerContent answer:
                    result.Response += answer.Text;
                    onUpdate?.Invoke(result.Response);
                    await Task.Delay(1); // brief yield for UI responsiveness
                    break;
                case RetrievalContent retrieval:
                    result.OriginalResources = retrieval.Results?.Resources as object;
                    break;
                case CitationsContent citations:
                    result.Citations = citations.Citations.Index();
                    break;
            }
        }

        return result;
    }
}
