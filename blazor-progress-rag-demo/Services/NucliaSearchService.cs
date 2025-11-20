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

    public NucliaSearchService(NucliaDbClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Sends an ask request to NucliaDb and streams the response.
    /// Each call maintains separate state for response, resources, and citations.
    /// </summary>
    /// <param name="query">The query string to ask</param>
    /// <param name="topK">The number of top results to return (default: 5)</param>
    /// <param name="cancellationToken">Cancellation token for the async operation</param>
    /// <returns>A StreamingAskResult containing the response, original resources, and citations</returns>
    public async Task<StreamingAskResult> AskAsync(string query, int topK = 5, CancellationToken cancellationToken = default)
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
