namespace Questao2.Services;

using Questao2.Interfaces;
using Questao2.Models;

public class GoalCalculationService : IGoalCalculationService
{
    private readonly IFootballMatchesClient _matchesClient;

    public GoalCalculationService(IFootballMatchesClient matchesClient)
    {
        _matchesClient = matchesClient;
    }

    public async Task<IEnumerable<TeamYearStats>> CalculateGoalsForTeamsAsync(
        IEnumerable<(string Team, int Year)> teamYears)
    {
        var results = new List<TeamYearStats>();

        foreach (var (team, year) in teamYears)
        {
            var goals = await _matchesClient.GetTotalGoalsForTeamAsync(team, year);
            results.Add(new TeamYearStats(team, year, goals));
        }

        return results;
    }
}