using System.Globalization;

namespace Questao1
{
    public class ContaBancaria
    {
        public int Numero { get; }
        public string nomeTitular { get; private set; }
        private double Saldo;
        private const double TAXA_SAQUE = 3.50;

        public ContaBancaria(int numero, string nomeTitular, double depositoInicial)
        {
            Numero = numero;
            this.nomeTitular = nomeTitular;
            Saldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            nomeTitular = titular;
            Saldo = 0.0;
        }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            Saldo -= quantia + TAXA_SAQUE;
        }

        public void AlterarNomeTitular(string novoTitular)
        {
            nomeTitular = novoTitular;
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {nomeTitular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}
