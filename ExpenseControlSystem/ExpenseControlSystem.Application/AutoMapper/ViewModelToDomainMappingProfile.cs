using AutoMapper;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PersonViewModel,Person>();
            CreateMap<TransactionViewModel, Transaction>();
        }
    }
}