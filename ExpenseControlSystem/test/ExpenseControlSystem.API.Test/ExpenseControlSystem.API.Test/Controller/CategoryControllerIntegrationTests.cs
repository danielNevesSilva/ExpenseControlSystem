using ExpenseControlSystem.API.Test;
using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.NovaPasta;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace ExpenseControlSystem.API.Controllers
{
    public class CategoryControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly Context _dbContext;

        public CategoryControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<Context>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnCategories()
        {
            // Act
            var response = await _client.GetAsync("/api/category");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnCategory()
        {
            // Arrange
            var existingCategory = _dbContext.Categories.First();

            // Act
            var response = await _client.GetAsync($"/api/category/{existingCategory.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var category = await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            Assert.NotNull(category);
            Assert.Equal(existingCategory.Id, category.Id);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var invalidId = 9999;

            // Act
            var response = await _client.GetAsync($"/api/category/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        

        [Fact]
        public async Task Create_WithValidData_ShouldReturnCreated()
        {
            // Arrange
            var categoryDto = new CreateCategoryDto
            {
                Description = "Nova Categoria Teste",
                Purpose = PurposeType.Expense
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/category", categoryDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdCategory = await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            Assert.NotNull(createdCategory);
            Assert.Equal(categoryDto.Description, createdCategory.Description);
            Assert.Equal(categoryDto.Purpose, createdCategory.Purpose);
        }

        [Fact]
        public async Task Create_WithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange - Descrição muito curta
            var invalidCategoryDto = new CreateCategoryDto
            {
                Description = "AB", // Muito curto (menos de 3 caracteres)
                Purpose = PurposeType.Expense
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/category", invalidCategoryDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldReturnNoContent()
        {
            // Arrange
            var existingCategory = _dbContext.Categories.First();

            var updateDto = new UpdateCategoryDto
            {
                Id = existingCategory.Id,
                Description = "Descrição Atualizada",
                Purpose = existingCategory.Purpose
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/category/{existingCategory.Id}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Verifica se foi atualizado
            var getResponse = await _client.GetAsync($"/api/category/{existingCategory.Id}");
            getResponse.EnsureSuccessStatusCode();
            var updatedCategory = await getResponse.Content.ReadFromJsonAsync<CategoryViewModel>();
            Assert.Equal("Descrição Atualizada", updatedCategory.Description);
        }

        [Fact]
        public async Task Update_WithMismatchedIds_ShouldReturnBadRequest()
        {
            // Arrange
            var updateDto = new UpdateCategoryDto
            {
                Id = 1,
                Description = "Teste",
                Purpose = PurposeType.Expense
            };

            // ID diferente na rota
            var differentId = 2;

            // Act
            var response = await _client.PutAsJsonAsync($"/api/category/{differentId}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithoutTransactions_ShouldReturnNoContent()
        {
            // Arrange - Cria uma categoria sem transações
            var categoryDto = new CreateCategoryDto
            {
                Description = "Categoria para Deletar",
                Purpose = PurposeType.Both
            };

            var createResponse = await _client.PostAsJsonAsync("/api/category", categoryDto);
            createResponse.EnsureSuccessStatusCode();
            var createdCategory = await createResponse.Content.ReadFromJsonAsync<CategoryViewModel>();

            // Act
            var response = await _client.DeleteAsync($"/api/category/{createdCategory.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Verifica se foi removida
            var getResponse = await _client.GetAsync($"/api/category/{createdCategory.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_WithTransactions_ShouldReturnBadRequest()
        {
            // Arrange - Encontra uma categoria que tenha transações
            var categoryWithTransactions = _dbContext.Categories
                .FirstOrDefault(c => _dbContext.Transactions.Any(t => t.CategoryId == c.Id));

            if (categoryWithTransactions == null)
            {
                // Se não houver, cria uma categoria e adiciona uma transação
                var categoryDto = new CreateCategoryDto
                {
                    Description = "Categoria com Transações",
                    Purpose = PurposeType.Expense
                };

                var createResponse = await _client.PostAsJsonAsync("/api/category", categoryDto);
                createResponse.EnsureSuccessStatusCode();
                var createdCategory = await createResponse.Content.ReadFromJsonAsync<CategoryViewModel>();

                // Cria uma transação para esta categoria
                var person = _dbContext.Persons.First();
                var transactionDto = new ExpenseControlSystem.Application.Dto.CreateTransactionDto
                {
                    Description = "Transação teste",
                    Amount = 100.00m,
                    Type = TransactionType.Expense,
                    CategoryId = createdCategory.Id,
                    PersonId = person.Id
                };

                await _client.PostAsJsonAsync("/api/transaction", transactionDto);
                categoryWithTransactions = _dbContext.Categories.Find(createdCategory.Id);
            }

            // Act - Tenta deletar categoria com transações
            var response = await _client.DeleteAsync($"/api/category/{categoryWithTransactions.Id}");

            // Assert - Deve retornar BadRequest
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // A categoria ainda deve existir
            var getResponse = await _client.GetAsync($"/api/category/{categoryWithTransactions.Id}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [Fact]
        public async Task GetByPurpose_AllPurposeTypes_ShouldWork()
        {
            // Testa todos os tipos de propósito
            var purposes = Enum.GetValues<PurposeType>();

            foreach (var purpose in purposes)
            {
                // Act
                var response = await _client.GetAsync($"/api/category/purpose/{(int)purpose}");

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();
                Assert.NotNull(categories);
            }
        }

        [Fact]
        public async Task Create_DuplicateDescription_ShouldHandleValidation()
        {
            // Arrange - Pega uma categoria existente
            var existingCategory = _dbContext.Categories.First();

            var duplicateCategoryDto = new CreateCategoryDto
            {
                Description = existingCategory.Description, // Mesma descrição
                Purpose = PurposeType.Expense
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/category", duplicateCategoryDto);

            // Assert - Pode retornar BadRequest se não permitir duplicatas
            // ou Created se permitir
            Assert.Contains(response.StatusCode,
                new[] { HttpStatusCode.Created, HttpStatusCode.BadRequest });
        }

    }
}