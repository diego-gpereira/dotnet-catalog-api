using FluentValidation.AspNetCore;
using FluentValidation;
using CatalogoApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra o serviço para que a aplicação entenda e use o padrão de Controllers.
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

// sempre que uma requisição HTTP chegar, antes de executar o código do meu controller, 
// execute automaticamente o validador correspondente (ex: ProdutoValidator)
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configura o Entity Framework para usar um banco de dados em memória.
// builder.Services.AddDbContext<CatalogoContext>(options =>
//     options.UseInMemoryDatabase("CatalogoDB"));
// Configura o Entity Framework para usar o SQL Server.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseSqlServer(connectionString));

// registra os serviços para o Swagger gerar a documentação da API.
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