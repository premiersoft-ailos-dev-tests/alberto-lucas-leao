namespace Questao5.Infrastructure.Database.Repository.Querys
{
    public class ContaCorrenteQuery
    {
        public const string ObterContaCorrente =
            @"
                SELECT * 
                FROM contacorrente
                WHERE numero = @Numero;
            ";
    }
}
