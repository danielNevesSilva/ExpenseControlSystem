using AutoMapper;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Aplications
{
    public class AppPerson : IAppPerson
    {
        protected readonly IMapper _mapper;
        protected readonly IServicePerson _servicePerson;

        public AppPerson(IServicePerson servicePerson, IMapper mapper)
        {
            _mapper = mapper;
            _servicePerson = servicePerson;

        }
        public async Task AddAsync(PersonViewModel entity)
        {
            var v = _mapper.Map<Person>(entity);
            _servicePerson.AddAsync(v);
        }
        public async Task UpdateAsync(PersonViewModel entity)
        {
            var v = _mapper.Map<Person>(entity);
            _servicePerson.UpdateAsync(v);
        }

        public async Task DeleteAsync(PersonViewModel entity)
        {
            var v = _mapper.Map<Person>(entity);
            _servicePerson.DeleteAsync(v);
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllAsync()
        {
            var v = await _servicePerson.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonViewModel>>(v);
        }

        public async Task<PersonViewModel> GetByIdAsync(int id)
        {
            var v = await _servicePerson.GetByIdAsync(id);
            return _mapper.Map<PersonViewModel>(v);
        }
        public async Task<PersonViewModel> GetByIdWithTransactionsAsync(int id)
        {
            var v = await _servicePerson.GetByIdWithTransactionsAsync(id);
            return _mapper.Map<PersonViewModel>(v);
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllWithTransactionsAsync()
        {
            var v = await _servicePerson.GetAllWithTransactionsAsync();
            return _mapper.Map<IEnumerable<PersonViewModel>>(v);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var v = await _servicePerson.ExistsAsync(id);
            return v;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        
    }
}
