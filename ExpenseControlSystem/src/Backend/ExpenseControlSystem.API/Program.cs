using ExpenseControlSystem.IoC;

var builder = WebApplication.CreateBuilder(args);


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

NativeInjectorBootStrapper.RegiiterServices(builder.Services);

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
