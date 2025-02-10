using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Application
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependences(this IServiceCollection services) 
        {
            //Repository
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();

            return services;
        }
    }
}
