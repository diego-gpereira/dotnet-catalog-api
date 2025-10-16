using FluentValidation.AspNetCore;
using FluentValidation;
using CatalogoApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra o serviço para que a aplicação entenda e use o padrão de Controllers.
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configura o Entity Framework para usar um banco de dados em memória.
builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseInMemoryDatabase("CatalogoDB"));

// Registra os serviços necessários para o Swagger gerar a documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();