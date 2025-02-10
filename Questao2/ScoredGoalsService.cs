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

    public async Task<int> GetGoals(int year, string team)
    {
        var url = $"{urlBase}?year={year}&team1={team}";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) throw new Exception($"{response.StatusCode} - Não foi possível acessar a API");

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Match>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (result == null) throw new Exception($"Não foi possível obter a quantidade de goals");

        var Goals = 0;
        foreach (var item in result.Data) 
        {
            Goals += int.Parse(item.Team1goals);
        }

        return Goals;
    }
}
