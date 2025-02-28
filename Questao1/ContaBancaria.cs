using System;
using System.ComponentModel.DataAnnotations;

namespace Questao1;

public class ContaBancaria
{
    public ContaBancaria() {}

    public ContaBancaria(int conta, string titular, double valor = 0)    
    {
        Conta = conta;
        Titular = titular;
        Saldo = valor;
    }

    [Required(ErrorMessage = "O número da conta deve ser informado")]
    public int Conta { get; set; }
    [Required(ErrorMessage = "O nome do ttular deve ser informado")]
    public string Titular { get; set; }
    
    private double Saldo;

    private const double TaxaSaque = 3.50;

    public double ObterSaldo() 
    {
        return Saldo;
    }

    public bool Deposito(double valor) 
    {
        if (valor <= 0)
        {
            Console.WriteLine("O depósito deve ser maior que 0");
            return false;
        }

        Saldo += valor;
        return true;
            
    }

    public bool Saque(double valor) 
    {
        if (valor <= 0) 
        {
            Console.WriteLine("Valor inválido para o saque");
            return false;
        }

        Saldo -= (valor + TaxaSaque);
        return true;
    }
};
