namespace Questao5.Infrastructure.Middleware.Querys
{
    public class IdempotenciaQuery
    {
        public const string SalvarIdempotencia =
            @"
                INSERT INTO idempotencia (
                    chave_idempotencia,
                    requisicao,
                    resultado
                )
                VALUES (
                    @IdRequisicao,
                    @Requisicao,
                    @Resultado
                );
            ";

        public const string ObterIdempotencia =
            @"
                SELECT *
                FROM idempotencia
                WHERE chave_idempotencia = @IdRequisicao;
            ";
    }
}
