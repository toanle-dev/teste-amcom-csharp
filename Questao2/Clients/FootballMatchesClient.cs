namespace Questao2.Clients;

using Questao2.Interfaces;
using Questao2.Models;
using System.Text.Json;

public class FootballMatchesClient : IFootballMatchesClient
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://jsonmock.hackerrank.com/api/football_matches";

    public FootballMatchesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetTotalGoalsForTeamAsync(string team, int year)
    {
        var goalsAsTeam1 = await GetGoalsForTeamInPositionAsync(team, year, "team1", "team1goals");
        var goalsAsTeam2 = await GetGoalsForTeamInPositionAsync(team, year, "team2", "team2goals");

        return goalsAsTeam1 + goalsAsTeam2;
    }

    private async Task<int> GetGoalsForTeamInPositionAsync(
        string team, int year, string teamField, string goalsField)
    {
        int totalGoals = 0;
        int page = 1;
        int totalPages = 1;

        do
        {
            var url = $"{ApiUrl}?year={year}&{teamField}={Uri.EscapeDataString(team)}&page={page}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse>(responseBody);

            totalPages = result.TotalPages;

            foreach (var match in result.Data)
            {
                if (match.TryGetProperty(goalsField, out var goalsProp) &&
                      int.TryParse(goalsProp.ToString(), out var goals))
                {
                    totalGoals += goals;
                }
            }

            page++;
        } while (page <= totalPages);

        return totalGoals;
    }
}