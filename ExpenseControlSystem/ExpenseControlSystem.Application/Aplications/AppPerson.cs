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
            var v = _servicePerson.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonViewModel>>(v);
        }

        public PersonViewModel GetByIdAsync(int id)
        {
            var v = _servicePerson.GetByIdAsync(id);
            return _mapper.Map<PersonViewModel>(v);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }


    }
}
