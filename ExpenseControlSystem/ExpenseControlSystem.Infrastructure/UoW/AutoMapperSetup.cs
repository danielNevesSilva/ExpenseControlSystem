using Microsoft.Extensions.DependencyInjection;

using ExpenseControlSystem.Application.AutoMapper;

namespace ExpenseControlSystem.Infrastructure.UoW
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            // Corrigido: Passando os tipos diretamente como params
            services.AddAutoMapper(AutoMapperConfiguration.RegisterMappings());
        }
    }
}