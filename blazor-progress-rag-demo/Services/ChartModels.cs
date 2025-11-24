using System.Text.Json.Serialization;

namespace blazor_progress_rag_demo.Services;

public class NucliaAskResponse
{
    [JsonPropertyName("answer_json")]
    public ChartAugmentedAnswer? AnswerJson { get; set; }

    [JsonPropertyName("answer")]
    public string? Answer { get; set; }
}

public class ChartAugmentedAnswer
{
    [JsonPropertyName("answer")]
    public string Answer { get; set; } = "";

    [JsonPropertyName("charts")]
    public List<ChartDataModel> Charts { get; set; } = new();
}

public class ChartDataModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = new();

    [JsonPropertyName("series")]
    public List<SeriesDataModel> Series { get; set; } = new();
}

public class SeriesDataModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("data")]
    public List<double> Data { get; set; } = new();
}
