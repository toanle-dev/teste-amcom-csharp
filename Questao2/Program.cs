namespace Questao2;


using Questao2.Clients;
using Questao2.Interfaces;
using Questao2.Services;
using Microsoft.Extensions.DependencyInjection;



class Program
{
    static async Task Main(string[] args)
    {
        // Configuração da injeção de dependência
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        // Obter o serviço
        var goalCalculationService = serviceProvider.GetRequiredService<IGoalCalculationService>();

        // Times e anos a serem pesquisados
        var teamYears = new List<(string Team, int Year)>
        {
            ("Paris Saint-Germain", 2013),
            ("Chelsea", 2014),
        };

        // Calcular e exibir resultados
        var results = await goalCalculationService.CalculateGoalsForTeamsAsync(teamYears);

        foreach (var result in results)
        {
            Console.WriteLine($"Team {result.Team} scored {result.Goals} goals in {result.Year}");
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddTransient<IFootballMatchesClient, FootballMatchesClient>();
        services.AddTransient<IGoalCalculationService, GoalCalculationService>();
    }
}