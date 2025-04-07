namespace Questao2.Interfaces;

public interface IFootballMatchesClient
{
    Task<int> GetTotalGoalsForTeamAsync(string team, int year);
}