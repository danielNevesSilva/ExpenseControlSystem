using ExpenseControlSystem.Infrastructure.Data;
using ExpenseControlSystem.IoC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<Context>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
else
{

    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("Default"));
    });
}

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

NativeInjectorBootStrapper.RegiterServices(builder.Services);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger"; // padrão
    });
}

// opcional
app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthorization();

// API endpoints
app.MapControllers();

app.Run();

public partial class Program
{
    protected Program() { }
}