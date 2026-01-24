using CommumUtilTest.Category;
using CommumUtilTest.Person;
using CommumUtilTest.Transaction;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Enums;
using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ExpenseControlSystem.API.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private static readonly Random _random = new Random();

        int personId = Generate();
        int categoryId = Generate();
        int transactionId = Generate();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    // Remove o DbContext atual
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<Context>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Configura o InMemory Database
                    services.AddDbContext<Context>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    // Build do ServiceProvider
                    var sp = services.BuildServiceProvider();

                    // Cria o escopo e inicializa o banco
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<Context>();

                        // Limpa e recria o banco
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        db.ChangeTracker.Clear();
                        StartDatabase(db);
                    }

                });
        }
        private void StartDatabase(Context dbContext)
        {
            try
            {
                dbContext.ChangeTracker.Clear();
                var personBuilder = new PersonViewModelBuilder();
                var persons = personBuilder.BuildList(5);

                foreach (var personVm in persons)
                {
                    var personEntity = new Person
                    {
                        Id = personId++,
                        Name = personVm.Name,
                        Age = personVm.Age,
                        CreatedAt = personVm.CreatedAt,
                        UpdatedAt = personVm.UpdatedAt
                    };

                    dbContext.Persons.Add(personEntity);
                }


                var categoryBuilder = new CategoryViewModelBuilder();
                var categories = categoryBuilder.BuildList(3);

                foreach (var categoryVm in categories)
                {
                    var categoryEntity = new Category
                    {
                        Id = categoryId++,
                        Description = categoryVm.Description,
                        Purpose = categoryVm.Purpose,
                        CreatedAt = categoryVm.CreatedAt,
                        UpdatedAt = categoryVm.UpdatedAt
                    };

                    dbContext.Categories.Add(categoryEntity);
                }


                dbContext.SaveChanges();

                var transactionBuilder = new TransactionViewModelBuilder();
                var transactions = transactionBuilder.BuildList(10);

                var dbPersons = dbContext.Persons.ToList();
                var dbCategories = dbContext.Categories.ToList();


                var random = new Random();

                foreach (var person in dbPersons)
                {

                    for (int i = 0; i < random.Next(2, 4); i++)
                    {
                        var category = dbCategories[random.Next(dbCategories.Count)];
                        var transactionVm = transactionBuilder.Build();

                        var transactionEntity = new Transaction
                        {
                            Id = transactionId++,
                            Description = transactionVm.Description,
                            Amount = transactionVm.Amount,
                            Type = transactionVm.Type,
                            CategoryId = category.Id,
                            PersonId = person.Id,
                            CreatedAt = transactionVm.CreatedAt,
                            UpdatedAt = transactionVm.UpdatedAt
                        };

                        dbContext.Transactions.Add(transactionEntity);
                    }
                }

                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding database: {ex.Message}");
                throw;
            }
        }

        public static int Generate()
        {
            var guid = Guid.NewGuid();
            var bytes = guid.ToByteArray();
            int id = BitConverter.ToInt32(bytes, 0);

            return Math.Abs(id % 90000) + 10000; 
        }
    }

}
