using AutoMapper;

using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Enums;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Application.Aplications
{
    public class AppCategory : IAppCategory
    {
        protected readonly IMapper _mapper;
        protected readonly IServiceCategory _categoryPerson;

        public AppCategory(IServiceCategory categoryPerson, IMapper mapper)
        {
            _mapper = mapper;
            _categoryPerson = categoryPerson;
        }


        public async Task<IEnumerable<CategoryViewModel>> GetByPurposeAsync(PurposeType purpose)
        {
            var v = await _categoryPerson.GetByPurposeAsync(purpose);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(v);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
