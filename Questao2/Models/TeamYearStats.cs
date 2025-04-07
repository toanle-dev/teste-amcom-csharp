namespace Questao2.Models;

public class TeamYearStats
{
    public string Team { get; }
    public int Year { get; }
    public int Goals { get; }

    public TeamYearStats(string team, int year, int goals)
    {
        Team = team;
        Year = year;
        Goals = goals;
    }
}