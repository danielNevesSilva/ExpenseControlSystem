using AutoMapper;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<Transaction, TransactionViewModel>();
        }
    }
}