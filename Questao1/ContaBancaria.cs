using System.ComponentModel.DataAnnotations;

namespace Questao1;

public class ContaBancaria
{
    public ContaBancaria() {}

    public ContaBancaria(int conta, string titular,double valor = 0)    
    {
        Conta = conta;
        Titular = titular;
        Saldo = valor;
    }

    [Required]
    public int Conta { get; set; }
    [Required]
    public string Titular { get; set; }
    public double Saldo { get; set; }

    public void Deposito(double quantia) 
    { 
        Saldo += quantia;
    }

    public void Saque(double quantia) 
    {
        quantia += 3.50;        
        Saldo -= quantia;      
    }
};
