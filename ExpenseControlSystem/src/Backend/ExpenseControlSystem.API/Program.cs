using ExpenseControlSystem.IoC;

var builder = WebApplication.CreateBuilder(args);

// API Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
NativeInjectorBootStrapper.RegiiterServices(builder.Services);

var app = builder.Build();

// Pipeline
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

app.UseAuthorization();

// API endpoints
app.MapControllers();

app.Run();
