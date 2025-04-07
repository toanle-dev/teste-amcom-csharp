namespace Questao5.Domain.Entities;

public class Movimento
{
    public string IdMovimento { get; set; } = string.Empty;
    public string IdContaCorrente { get; set; } = string.Empty;
    public string DataMovimento { get; set; } = string.Empty;
    // 'C' ou 'D'
    public string TipoMovimento { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
