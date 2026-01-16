using ExpenseControlSystem.Application.Aplications;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Domain.Interfaces.Service;
using ExpenseControlSystem.Domain.Services;
using ExpenseControlSystem.Infrastructure.Data;
using ExpenseControlSystem.Infrastructure.Repository;
using ExpenseControlSystem.Infrastructure.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseControlSystem.Infrastructure
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegiiterServices(IServiceCollection service)
        {
            service.AddAutoMapperSetup();


            service.AddScoped<IUnitOfWork, UnitOfWork>();

            service.AddScoped<IPersonRepository, RepositoryPerson>();
            service.AddScoped<IAppPerson,AppPerson>();
            service.AddScoped<IServicePerson,ServicePerson>();

            service.AddScoped<ITransactionRepository, RepositoryTransaction>();
            service.AddScoped<IAppTransaction, AppTransaction>();
            service.AddScoped<IServiceTransaction, ServiceTransaction>();

            service.AddDbContext<Context>();
        }
    }
}
