

using ExpenseControlSystem.Application.Aplications;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.Validation;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Domain.Interfaces.Service;
using ExpenseControlSystem.Domain.Services;
using ExpenseControlSystem.Infrastructure.Data;
using ExpenseControlSystem.Infrastructure.Repository;
using ExpenseControlSystem.Infrastructure.UoW;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseControlSystem.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegiiterServices(IServiceCollection service)
        {
            service.AddAutoMapperSetup();

            service.AddScoped<IUnitOfWork, UnitOfWork>();

            service.AddScoped<IPersonRepository, RepositoryPerson>();
            service.AddScoped<IAppPerson, AppPerson>();
            service.AddScoped<IServicePerson, ServicePerson>();

            service.AddScoped<ITransactionRepository, RepositoryTransaction>();
            service.AddScoped<IAppTransaction, AppTransaction>();
            service.AddScoped<IServiceTransaction, ServiceTransaction>();

            service.AddScoped<ICategoryRepository, RepositoryCategory>();
            service.AddScoped<IAppCategory, AppCategory>();
            service.AddScoped<IServiceCategory, ServiceCategory>();

            service.AddScoped<IValidator<PersonViewModel>, PersonValidation>();
            service.AddScoped<IValidator<TransactionViewModel>, TransactionValidation>();
            service.AddScoped<IValidator<CategoryViewModel>, CategoryValidation>();

            service.AddDbContext<Context>();

        }
    }
}
