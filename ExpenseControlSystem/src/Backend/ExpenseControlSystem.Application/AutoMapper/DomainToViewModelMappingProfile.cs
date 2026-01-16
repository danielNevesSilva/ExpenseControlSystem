using AutoMapper;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Person, PersonViewModel>()
                  .ForMember(dest => dest.Transactions, opt => opt.Ignore());

            // Mapeamento de Category
            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.Purpose,
                    opt => opt.MapFrom(src => src.Purpose.ToString()));

            // Mapeamento de Transaction
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.CategoryDescription,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Description : string.Empty))
                .ForMember(dest => dest.PersonName,
                    opt => opt.MapFrom(src => src.Person != null ? src.Person.Name : string.Empty))
                .ForMember(dest => dest.Category,
                    opt => opt.Ignore()) // Não incluir objeto completo
                .ForMember(dest => dest.Person,
                    opt => opt.Ignore()); // Não incluir objeto completo

            // Mapeamento para respostas simplificadas
            CreateMap<Person, PersonWithTransactionsViewModel>()
                .ForMember(dest => dest.Transactions,
                    opt => opt.MapFrom(src => src.Transactions));
        }
    }
}