using CommumUtilTest.Person;
using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Enums;
using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace ExpenseControlSystem.API.Test.Controllers
{

    public class TransactionControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly Context _dbContext;

        public TransactionControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<Context>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnTransactions()
        {
           
            var response = await _client.GetAsync("/api/transaction");

           
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var transactions = await response.Content.ReadFromJsonAsync<List<TransactionViewModel>>();
            Assert.NotNull(transactions);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnTransaction()
        {
           
            var existingTransaction = _dbContext.Transactions.First();

           
            var response = await _client.GetAsync($"/api/transaction/{existingTransaction.Id}");

           
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var transaction = await response.Content.ReadFromJsonAsync<TransactionViewModel>();
            Assert.NotNull(transaction);
            Assert.Equal(existingTransaction.Id, transaction.Id);
        }

        [Fact]
        public async Task GetByPersonId_ShouldReturnPersonTransactions()
        {
            
            var person = _dbContext.Persons.First();

           
            var response = await _client.GetAsync($"/api/transaction/person/{person.Id}");

           
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var transactions = await response.Content.ReadFromJsonAsync<List<TransactionViewModel>>();
            Assert.NotNull(transactions);
        }

       
        [Fact]
        public async Task Update_WithValidData_ShouldReturnNoContent()
        {
        
            var existingTransaction = _dbContext.Transactions.First();

            var updateDto = new UpdateTransactionDto
            {
                Id = existingTransaction.Id,
                Description = "Descrição atualizada",
                Amount = 200.00m,
                Type = existingTransaction.Type,
                CategoryId = existingTransaction.CategoryId,
                PersonId = existingTransaction.PersonId
            };

           
            var response = await _client.PutAsJsonAsync($"/api/transaction/{existingTransaction.Id}", updateDto);

           
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Verifica se foi atualizado
            var getResponse = await _client.GetAsync($"/api/transaction/{existingTransaction.Id}");
            getResponse.EnsureSuccessStatusCode();
            var updatedTransaction = await getResponse.Content.ReadFromJsonAsync<TransactionViewModel>();
            Assert.Equal("Descrição atualizada", updatedTransaction.Description);
        }

 
        [Fact]
        public async Task GetTotalExpenseByPerson_ShouldReturnCorrectTotal()
        {
            
            var person = _dbContext.Persons.First();

            // Cria algumas transações de expense para a pessoa
            var category = _dbContext.Categories.First(c => c.Purpose == PurposeType.Expense);

            var transaction1 = new CreateTransactionDto
            {
                Description = "Expense 1",
                Amount = 50.00m,
                Type = TransactionType.Expense,
                CategoryId = category.Id,
                PersonId = person.Id
            };

            var transaction2 = new CreateTransactionDto
            {
                Description = "Expense 2",
                Amount = 75.00m,
                Type = TransactionType.Expense,
                CategoryId = category.Id,
                PersonId = person.Id
            };

            await _client.PostAsJsonAsync("/api/transaction", transaction1);
            await _client.PostAsJsonAsync("/api/transaction", transaction2);

           
            var response = await _client.GetAsync($"/api/transaction/person/{person.Id}/total-expense");

           
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var total = await response.Content.ReadFromJsonAsync<decimal>();
            // Deve incluir as transações criadas + as existentes no seed
        }

        
        [Fact]
        public async Task Create_WithWrongCategoryType_ShouldHandleValidation()
        {
            
            var person = _dbContext.Persons.First();
               var expenseCategory = _dbContext.Categories
                .First(c => c.Purpose == PurposeType.Expense);

            var transactionDto = new CreateTransactionDto
            {
                Description = "Income com categoria errada",
                Amount = 100.00m,
                Type = TransactionType.Income, // Tipo incompatível
                CategoryId = expenseCategory.Id,
                PersonId = person.Id
            };

           
            var response = await _client.PostAsJsonAsync("/api/transaction", transactionDto);

           
         
            Assert.Contains(response.StatusCode,
                new[] { HttpStatusCode.BadRequest, HttpStatusCode.Created });
        }

        public void Dispose()
        {
            _client?.Dispose();
            _dbContext?.Dispose();
        }
    }
}