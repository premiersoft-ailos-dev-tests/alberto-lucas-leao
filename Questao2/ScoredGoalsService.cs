using System.Text.Json;

namespace Questao2;

public class ScoredGoalsService
{
    private readonly HttpClient _httpClient;
    private const string urlBase = "https://jsonmock.hackerrank.com/api/football_matches";

    public ScoredGoalsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetTeams() 
    {
        var response = await _httpClient.GetStringAsync(urlBase);
        var result = JsonSerializer.Deserialize<Match>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var times = new HashSet<string>();
        if(result?.Data == null) throw new Exception($"Unable to get teams");

        foreach (var item in result.Data)
        {
            times.Add(item.Team1);
            times.Add(item.Team2);
        }

        return new List<string>(times);
    }

    public async Task<int> GetGoals(string team1, string? team2, int year, int? page)
    {
        var url = $"{urlBase}?year={year}&team1={team1}";
        if (!string.IsNullOrEmpty(team2)) url += $"&team2={team2}";
        if (page.HasValue) url += $"&page={page}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) throw new Exception($"{response.StatusCode} - Unable to access API");

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Match>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (result == null) throw new Exception($"Unable to obtain the number of goals");

        var Goals = 0;
        foreach (var item in result.Data) 
        {
            Goals += int.Parse(item.Team1goals);
        }

        return Goals;
    }
}
