using ExpenseControlSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Infrastructure.Data
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das entidades
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Age).IsRequired();

                entity.HasMany(p => p.Transactions)
                      .WithOne(t => t.Person)
                      .HasForeignKey(t => t.PersonId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascata para deleção
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Description).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Purpose)
                      .HasConversion<string>()
                      .IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Description).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Amount).HasPrecision(18, 2);
                entity.Property(t => t.Type).HasConversion<string>().IsRequired();

                entity.HasOne(t => t.Category)
                      .WithMany()
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict); // Não permite deletar categoria com transações
            });
        }
    }
}
