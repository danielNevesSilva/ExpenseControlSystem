# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore ExpenseControlSystem.API/ExpenseControlSystem.API.csproj
RUN dotnet test ExpenseControlSystem.API/ExpenseControlSystem.API.csproj --no-restore
RUN dotnet publish ExpenseControlSystem.API/ExpenseControlSystem.API.csproj -c Release -o /app/publish

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExpenseControlSystem.API.dll"]
