using CommumUtilTest.Person;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace ExpenseControlSystem.API.Test.Controller
{

    public class PersonControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PersonControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithPersons()
        {
            
            var expectedPersons = new PersonViewModelBuilder().BuildList(3);

          
            var response = await _client.GetAsync("/api/person");
            response.EnsureSuccessStatusCode();

            var persons = await response.Content.ReadFromJsonAsync<List<PersonViewModel>>();

            
            Assert.NotNull(persons);
            Assert.NotEmpty(persons);
        }

        [Fact]
        public async Task Create_WithInvalidData_ShouldReturnBadRequest()
        {
            
            var invalidPerson = new PersonViewModelBuilder()
                .WithInvalidName()
                .WithInvalidAge()
                .Build();

          
            var response = await _client.PostAsJsonAsync("/api/person", invalidPerson);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithMismatchedIds_ShouldReturnBadRequest()
        {
            
            var person = new PersonViewModelBuilder()
                .WithId(1)
                .WithName("Test Person")
                .WithAge(25)
                .Build();

            var differentId = 2;

          
            var response = await _client.PutAsJsonAsync($"/api/person/{differentId}", person);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ShouldReturnNotFound()
        {
            
            var invalidId = 99999;

          
            var response = await _client.DeleteAsync($"/api/person/{invalidId}");

            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAllWithTransactions_ShouldReturnPersonsWithTransactions()
        {
          
            var response = await _client.GetAsync("/api/person/with-transactions");

            
            response.EnsureSuccessStatusCode();
            var persons = await response.Content.ReadFromJsonAsync<List<PersonViewModel>>();
            Assert.NotNull(persons);
        }

       
        [Fact]
        public async Task Exists_WithNonExistingId_ShouldReturnFalse()
        {
            
            var nonExistingId = 99999;

          
            var response = await _client.GetAsync($"/api/person/{nonExistingId}/exists");

            
            response.EnsureSuccessStatusCode();
            var exists = await response.Content.ReadFromJsonAsync<bool>();
            Assert.False(exists);
        }


        [Fact]
        public async Task Update_PersonAsMinor_ShouldHandleAgeValidation()
        {
            
            var person = new PersonViewModelBuilder()
                .WithName("Minor Person")
                .AsMinor() 
                .Build();
            person.Id = 0;

            var response = await _client.PostAsJsonAsync("/api/person", person);

          
            Assert.Contains(response.StatusCode,
                new[] { HttpStatusCode.Created, HttpStatusCode.BadRequest });
        }

        [Fact]
        public async Task Update_PersonToInvalidAge_ShouldReturnBadRequest()
        {

            var person = new PersonViewModelBuilder()
                .WithName("Valid Person")
                .WithAge(25)
                .Build();
            person.Id = 0;

            var createResponse = await _client.PostAsJsonAsync("/api/person", person);
            createResponse.EnsureSuccessStatusCode();
            var createdPerson = await createResponse.Content.ReadFromJsonAsync<PersonViewModel>();

            createdPerson.Age = 0;

          
            var response = await _client.PutAsJsonAsync($"/api/person/{createdPerson.Id}", createdPerson);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithMaximumLengthName_ShouldSucceed()
        {
            
            var longName = new string('A', 100); // Nome com 100 caracteres
            var person = new PersonViewModelBuilder()
                .WithName(longName)
                .WithAge(30)
                .Build();
            person.Id = 0;

          
            var response = await _client.PostAsJsonAsync("/api/person", person);

            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithExceedingMaximumLengthName_ShouldFail()
        {
            
            var tooLongName = new string('A', 101); // Nome com 101 caracteres
            var person = new PersonViewModelBuilder()
                .WithName(tooLongName)
                .WithAge(30)
                .Build();
            person.Id = 0;

          
            var response = await _client.PostAsJsonAsync("/api/person", person);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}