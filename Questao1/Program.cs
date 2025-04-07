using System;
using System.Globalization;

namespace Questao1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Conta bancaria
            ContaBancaria conta;


            // Cria conta bancaria
            Console.Clear();
            Console.WriteLine("# Criar Conta #");

            Console.Write("\nEntre o número da conta*: ");
            int numero;
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.WriteLine("Por favor, digite um número válido.");
            }

            Console.Write("Entre o titular da conta*: ");
            string titular = Console.ReadLine() ?? "";
            while (string.IsNullOrWhiteSpace(titular))
            {
                Console.WriteLine("Por favor, digite um nome válido.");
                titular = Console.ReadLine() ?? "";
            }

            Console.Write("Haverá depósito inicial(s / n):");
            string depositoInicialResposta = Console.ReadLine() ?? "";
            while (string.IsNullOrWhiteSpace(depositoInicialResposta) || (depositoInicialResposta.ToLower() != "s" && depositoInicialResposta.ToLower() != "n"))
            {
                Console.WriteLine("Por favor, digite 's' ou 'n'.");
                depositoInicialResposta = Console.ReadLine() ?? "";
            }
            if (depositoInicialResposta.ToLower() == "n")
            {
                conta = new ContaBancaria(numero, titular);
            }
            else
            {
                Console.Write("Entre o valor de depósito inicial: ");
                double depositoInicial;
                while (!double.TryParse(Console.ReadLine(), out depositoInicial))
                {
                    Console.WriteLine("Por favor, digite um valor válido.");
                }
                conta = new ContaBancaria(numero, titular, depositoInicial);
            }

            Console.WriteLine("\nDados da conta:");
            Console.WriteLine(conta);



            // Faz depósito na conta bancaria
            Console.Write("\nEntre um valor para depósito: ");
            double valorDeposito;
            if (!double.TryParse(Console.ReadLine(), out valorDeposito))
            {
                valorDeposito = 0;
            }
            conta.Deposito(valorDeposito);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(conta);


            // Faz saque na conta bancaria
            Console.Write("\nEntre um valor para saque: ");
            double valorSaque;
            while (!double.TryParse(Console.ReadLine(), out valorSaque))
            {
                Console.WriteLine("Por favor, digite um valor válido.");
            }
            conta.Saque(valorSaque);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(conta);


            // Altera o nome do titular
            Console.WriteLine("\nDeseja alterar o nome do titular? (s/N)");
            string alterarNomeTitular = Console.ReadLine() ?? "";
            if (alterarNomeTitular.ToLower() == "s")
            {
                Console.WriteLine("\nDigite o novo nome do titular: ");
                string novoNome = Console.ReadLine() ?? "";
                while (string.IsNullOrWhiteSpace(novoNome))
                {
                    Console.WriteLine("Por favor, digite um nome válido.");
                    novoNome = Console.ReadLine() ?? "";
                }
                conta.AlterarNomeTitular(novoNome);
                Console.WriteLine("\nDados da conta atualizados:");
                Console.WriteLine(conta);
                Console.Read();
            }



            /* Output expected:
            Exemplo 1:

            Entre o número da conta: 5447
            Entre o titular da conta: Milton Gonçalves
            Haverá depósito inicial(s / n) ? s
            Entre o valor de depósito inicial: 350.00

            Dados da conta:
            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 350.00

            Entre um valor para depósito: 200
            Dados da conta atualizados:
            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 550.00

            Entre um valor para saque: 199
            Dados da conta atualizados:
            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 347.50

            Exemplo 2:
            Entre o número da conta: 5139
            Entre o titular da conta: Elza Soares
            Haverá depósito inicial(s / n) ? n

            Dados da conta:
            Conta 5139, Titular: Elza Soares, Saldo: $ 0.00

            Entre um valor para depósito: 300.00
            Dados da conta atualizados:
            Conta 5139, Titular: Elza Soares, Saldo: $ 300.00

            Entre um valor para saque: 298.00
            Dados da conta atualizados:
            Conta 5139, Titular: Elza Soares, Saldo: $ -1.50
            */
        }
    }
}
