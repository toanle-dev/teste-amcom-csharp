namespace Questao2.Interfaces;


using Questao2.Models;

public interface IGoalCalculationService
{
    Task<IEnumerable<TeamYearStats>> CalculateGoalsForTeamsAsync(IEnumerable<(string Team, int Year)> teamYears);
}