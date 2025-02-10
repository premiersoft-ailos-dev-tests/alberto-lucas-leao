using System.Text.Json.Serialization;

namespace Questao2;

public class Match
{
    public int Page { get; set; }
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
    public int Total { get; set; }
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
    public List<MatchResponse> Data { get; set; } = new List<MatchResponse>();
}

public class MatchResponse
{
    public string Competition { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Round { get; set; } = string.Empty;
    public string Team1 { get; set; } = string.Empty;        
    public string Team2 { get; set; } = string.Empty;        
    public string Team1goals { get; set; } = string.Empty;
    public string Team2goals { get; set; } = string.Empty;
}
