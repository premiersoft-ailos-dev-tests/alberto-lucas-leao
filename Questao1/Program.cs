﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Questao1; 
class Program {

    private static List<ContaBancaria> _contas = new();

    static void Main(string[] args) {

        ContaBancaria conta;
        string sair;
        string alterarNome;

        do
        {
            inicio:
            Console.Write("Entre com o número da conta: ");
            int numero = int.Parse(Console.ReadLine());
            
            var existeConta = ConsultarConta(numero);
            if (existeConta == true)
            {
                Console.WriteLine("Não é possivel criar a conta com o número informado: ");
                Console.WriteLine();
                goto inicio;
            }

            Console.Write("Entre com o titular da conta: ");
            string titular = Console.ReadLine();
            Console.Write("Haverá depósito inicial (s/n)? ");
            char resp = char.Parse(Console.ReadLine());
            if (resp == 's' || resp == 'S')
            {
                Console.Write("Entre com o valor de depósito inicial: ");
                double depositoInicial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                conta = new ContaBancaria(numero, titular, depositoInicial);
            }
            else
            {
                conta = new ContaBancaria(numero, titular);
            }

            Console.WriteLine();
            Console.WriteLine("Dados da conta:");
            Console.WriteLine(FormataConta(conta));

            Console.WriteLine();
            Console.Write("Entre com um valor para depósito: ");
            double quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            conta.Deposito(quantia);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(FormataConta(conta));

            Console.WriteLine();
            Console.Write("Entre com um valor para saque: ");
            quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            conta.Saque(quantia);
            
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(FormataConta(conta));

            _contas.Add(conta);

            Console.WriteLine();
            Console.WriteLine("Deseja alterar no nome do Titular (s/n)?: ");
            alterarNome = Console.ReadLine();

            if (alterarNome == "s") 
            {
                Console.Write("Entre com o novo nome: ");
                string novoNome = Console.ReadLine();

                AlterarNomeTitular(numero, novoNome);

                Console.WriteLine("Dados da conta atualizados:");
                Console.WriteLine(FormataConta(conta));
            }

            Console.WriteLine();
            Console.WriteLine("Digite s para sair ou enter para continuar: ");
            sair = Console.ReadLine();
        } while (sair?.ToLower() != "s");

        

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

    public static bool ConsultarConta(int numero) 
    {
        if(_contas == null) return false;
        var retorno = _contas.Any(x => x.Conta == numero);            
        return retorno;
    }

    public static string FormataConta(ContaBancaria conta) 
    {
        var valorFormatado = conta.Saldo.ToString("C", new CultureInfo("en-US"));
        return $"Conta {conta.Conta}, Titular: {conta.Titular}, Saldo: {valorFormatado}";
    }

    public static ContaBancaria AlterarNomeTitular(int conta, string novoNome) 
    {
        var retorno = _contas.FirstOrDefault(x => x.Conta == conta);
        retorno.Titular = novoNome;

        return retorno;
    }
}
