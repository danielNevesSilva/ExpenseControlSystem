using ExpenseControlSystem.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseControlSystem.IoC
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            // Corrigido: Passando os tipos diretamente como params
            services.AddAutoMapper(
                typeof(DomainToViewModelMappingProfile),
                typeof(ViewModelToDomainMappingProfile)
                );
        }
    }
}