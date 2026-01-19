using AutoMapper;
using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.Exceptions;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.NovaPasta;
using ExpenseControlSystem.Application.Validation;
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
            await ValidateRequestCreate(entity);
            var v = _mapper.Map<Category>(entity);
            await _categoryPerson.AddAsync(v);

        }

        public async Task UpdateAsync(UpdateCategoryDto entity)
        {
            await ValidateRequestUpdate(entity);
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

        public async Task ValidateRequest(CategoryViewModel model)
        {
            var v = new CategoryValidation();
            var result = await v.ValidateAsync(model);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }

        public async Task ValidateRequestUpdate(UpdateCategoryDto model)
        {
            var v = new UpdateCategoryValidation();
            var result = await v.ValidateAsync(model);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }

        public async Task ValidateRequestCreate(CreateCategoryDto model)
        {
            var v = new CreateCategoryValidation();
            var result = await v.ValidateAsync(model);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
