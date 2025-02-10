namespace Questao5.Infrastructure.Database.Repository.Querys
{
    public class MovimentacaoQuery
    {
        public const string AdicionarMovimentacao =
            @"
                INSERT INTO movimento (
                    idmovimento, 
                    idcontacorrente,
                    datamovimento,
                    tipomovimento,
                    valor
                ) 
                VALUES (
                    @IdMovimento,
                    @IdContaCorrente,
                    @DataMovimento,
                    @TipoMovimento,
                    @Valor
                );
            ";

        public const string ObterMovimentacao =
            @"
                SELECT * 
                FROM movimento
                WHERE idmovimento = @IdMovimento ;
            ";

        public const string ObterMovimentacoesContaCorrete =
            @"
                SELECT * 
                FROM movimento 
                WHERE idcontacorrente = @IdContaCorrente;
            ";
    }
}
