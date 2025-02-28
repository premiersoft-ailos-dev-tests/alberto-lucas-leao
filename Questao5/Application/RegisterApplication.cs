using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.Repository;
using System.Diagnostics.CodeAnalysis;

namespace Questao5.Application
{
    [ExcludeFromCodeCoverage]
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependences(this IServiceCollection services) 
        {
            //Repository
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
            services.AddScoped<ISaldoRepository, SaldoRepository>();

            return services;
        }
    }
}
