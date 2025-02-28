namespace Questao5.Infrastructure.Redis.Querys
{
    public class RedisQuery
    {
        public const string ObterIdContaMovimentacao = @"
            SELECT DISTINCT idcontacorrente 
            FROM movimento
        ";

        public const string ObterMovimentacaoConta = @"
            SELECT SUM(CASE WHEN tipomovimento = 'C' THEN Valor ELSE -Valor END) 
            FROM movimento 
            WHERE idcontacorrente = @IdConta
        ";
    }
}
