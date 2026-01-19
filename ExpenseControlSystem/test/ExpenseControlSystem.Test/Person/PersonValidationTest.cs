using CommumUtilTest.Person;
using ExpenseControlSystem.Application.Validation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Test.Person
{
    public class PersonValidationTest
    {
        private readonly PersonValidation _validator;
        public PersonValidationTest()
        {
            _validator = new PersonValidation();
        }

        [Fact]
        public void Should_Pass_When_Person_Is_Valid()
        {

            var person = PersonViewModelBuilder.Valid();

                
            var result = _validator.Validate(person);


            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Short()
        {

            var person = new PersonViewModelBuilder()
                .WithInvalidName()
                .Build();


            var result = _validator.TestValidate(person);


            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Fact]
        public void Should_Fail_When_Age_Is_Zero()
        {

            var person = new PersonViewModelBuilder()
                .WithInvalidAge()
                .Build();


            var result = _validator.TestValidate(person);


            result.ShouldHaveValidationErrorFor(p => p.Age);
        }

        [Fact]
        public void Should_Fail_When_Age_Is_Negative()
        {

            var person = new PersonViewModelBuilder()
                .WithAge(-5)
                .Build();


            var result = _validator.TestValidate(person);


            result.ShouldHaveValidationErrorFor(p => p.Age);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(150)]
        [InlineData(25)]
        [InlineData(99)]
        public void Should_Pass_When_Age_Is_Valid(int age)
        {

            var person = new PersonViewModelBuilder()
                .WithAge(age)
                .Build();


            var result = _validator.TestValidate(person);


            result.ShouldNotHaveValidationErrorFor(p => p.Age);
        }

    }
}
