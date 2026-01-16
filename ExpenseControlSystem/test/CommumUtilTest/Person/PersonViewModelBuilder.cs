using Bogus;
using ExpenseControlSystem.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Person
{
    public class PersonViewModelBuilder
    {
        private readonly Faker<PersonViewModel> _faker;
        private PersonViewModel _customPerson;

        public PersonViewModelBuilder()
        {
            _faker = new Faker<PersonViewModel>("pt_BR")
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .RuleFor(p => p.Name, f => f.Person.FullName)
                .RuleFor(p => p.Age, f => f.Random.Int(18, 70))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past())
                .RuleFor(p => p.UpdatedAt, f => f.Date.Recent().OrNull(f));
        }

        public PersonViewModelBuilder WithId(int id)
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Id = id;
            return this;
        }

        public PersonViewModelBuilder WithName(string name)
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Name = name;
            return this;
        }

        public PersonViewModelBuilder WithAge(int age)
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Age = age;
            return this;
        }

        public PersonViewModelBuilder AsMinor()
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Age = new Faker().Random.Int(1, 17);
            return this;
        }

        public PersonViewModelBuilder AsAdult()
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Age = new Faker().Random.Int(18, 70);
            return this;
        }

        public PersonViewModelBuilder WithInvalidName()
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Name = "A"; // Nome muito curto
            return this;
        }

        public PersonViewModelBuilder WithInvalidAge()
        {
            _customPerson ??= new PersonViewModel();
            _customPerson.Age = 0; // Idade inválida
            return this;
        }

        public PersonViewModel Build()
        {
            if (_customPerson != null)
            {
                var person = _customPerson;
                _customPerson = null; // Reset para próximo build
                return person;
            }

            return _faker.Generate();
        }

        public List<PersonViewModel> BuildList(int count = 5)
        {
            return _faker.Generate(count);
        }

        // Métodos estáticos para casos comuns
        public static PersonViewModel Valid()
        {
            return new PersonViewModelBuilder().Build();
        }


        public static PersonViewModel Adult()
        {
            return new PersonViewModelBuilder().AsAdult().Build();
        }

        public static PersonViewModel WithInvalidData()
        {
            return new PersonViewModelBuilder()
                .WithInvalidName()
                .WithInvalidAge()
                .Build();
        }
    }
}
