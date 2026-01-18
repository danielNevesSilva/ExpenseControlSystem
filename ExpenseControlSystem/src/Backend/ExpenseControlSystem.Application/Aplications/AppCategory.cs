using AutoMapper;

using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.NovaPasta;
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
        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {

            var v = await _categoryPerson.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(v);
        }
        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {

            var v = await _categoryPerson.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(v);
        }

        public async Task AddAsync(CreateCategoryDto entity)
        {
            var v = _mapper.Map<Category>(entity);
             await _categoryPerson.AddAsync(v);

        }

        public async Task UpdateAsync(CategoryViewModel entity)
        {
            var v = _mapper.Map<Category>(entity);
            _categoryPerson.UpdateAsync(v);

        }

        public async Task DeleteAsync(CategoryViewModel entity)
        {
            var v = _mapper.Map<Category>(entity);
            _categoryPerson.DeleteAsync(v);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
