namespace Questao2.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

public class ApiResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("data")]
    public List<JsonElement> Data { get; set; } = new List<JsonElement>();
}