using AutoMapper;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreatePersonViewModel, Person>()
                .ConstructUsing(src => new Person(src.Name, src.Age));

            // Para atualização
            CreateMap<PersonViewModel, Person>()
                .ForMember(dest => dest.Transactions, opt => opt.Ignore());

            CreateMap<CategoryViewModel, Category>();
            CreateMap<TransactionViewModel, Transaction>();
        }
    }
}