namespace Questao5.Application.Queries.Responses;

public class SaldoContaCorrenteResponse
{
    public int Numero { get; set; }
    public string NomeTitular { get; set; } = string.Empty;
    public DateTime DataHoraConsulta { get; set; }
    public decimal Saldo { get; set; }
}