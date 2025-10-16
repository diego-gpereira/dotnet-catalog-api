using CatalogoApi.Controllers;
using CatalogoApi.Data;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CatalogApi.Tests;

public class ProdutosControllerTests
{
    private readonly CatalogoContext _context;
    private readonly ProdutosController _controller;

    public ProdutosControllerTests()
    {
        var options = new DbContextOptionsBuilder<CatalogoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CatalogoContext(options);
        _controller = new ProdutosController(_context);

        _context.Categorias.Add(new Categoria { Id = 1, Nome = "Bebidas" });
        _context.SaveChanges();
    }

    // Padrão nomenclaturas dos métodos de teste: 
    // [NomeDoMetodoTestado]_[CenárioOuCondição]_[ResultadoEsperado]

    [Fact]
    public async Task PostProduto_ComDadosValidos_DeveRetornar201Created()
    {
        // Arrange
        var novoProduto = new Produto
        {
            Nome = "Refrigerante 2L",
            Descricao = "Refrigerante sabor cola",
            Preco = 8.50m,
            CategoriaId = 1
        };

        // Act
        var resultado = await _controller.PostProduto(novoProduto);

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultado.Result);
    }

    [Fact]
    public async Task PostProduto_ComPrecoInvalido_DeveRetornar400BadRequest()
    {
        // um produto com Preco = 0, que viola nossa regra de negócio
        var produtoInvalido = new Produto
        {
            Nome = "Produto com Erro",
            Descricao = "Este produto tem um preço inválido",
            Preco = 0m, // Preço inválido
            CategoriaId = 1
        };

        // adicionado manualmente o erro ao ModelState do controller para simular
        // o comportamento do ASP.NET Core ao receber dados inválidos.
        _controller.ModelState.AddModelError("Preco", "O preço deve ser maior que zero.");

        var resultado = await _controller.PostProduto(produtoInvalido);

        Assert.IsType<BadRequestObjectResult>(resultado.Result);
    }

    [Fact]
    public async Task GetProdutoPorId_ComIdInexistente_DeveRetornar404NotFound()
    {
        // não é necessário nenhuma preparação específica
        const int idInexistente = 999;

        var resultado = await _controller.GetProdutoPorId(idInexistente);

        Assert.IsType<NotFoundObjectResult>(resultado.Result);
    }

    [Fact]
    public async Task PutProduto_ComDadosValidos_DeveRetornar204NoContent()
    {
        // add produto ao banco de dados em memória para que ele exista
        var produtoOriginal = new Produto
        {
            Nome = "Produto Antigo",
            Descricao = "Descrição Original",
            Preco = 10m,
            CategoriaId = 1,
            DataCadastro = DateTime.UtcNow
        };
        _context.Produtos.Add(produtoOriginal);
        await _context.SaveChangesAsync();

        var produtoAtualizado = new Produto
        {
            Id = produtoOriginal.Id, // ID deve ser o mesmo
            Nome = "Produto Atualizado",
            Descricao = "Nova Descrição",
            Preco = 15.50m,
            CategoriaId = 1,
            DataCadastro = produtoOriginal.DataCadastro
        };

        // método PutProduto espera os parametros (id, [body])
        var resultado = await _controller.PutProduto(produtoOriginal.Id, produtoAtualizado);

        Assert.IsType<NoContentResult>(resultado);

        // verifica se os dados foram realmente alterados
        var produtoDoBanco = await _context.Produtos.FindAsync(produtoOriginal.Id);

        Assert.NotNull(produtoDoBanco);
        Assert.Equal("Produto Atualizado", produtoDoBanco.Nome);
        Assert.Equal(15.50m, produtoDoBanco.Preco);
    }

    [Fact]
    public async Task DeleteProduto_ComIdExistente_DeveRetornar204NoContentERemoverDoBanco()
    {
        // add produto alvo para excluir
        var produtoParaDeletar = new Produto
        {
            Nome = "Produto a ser Deletado",
            Preco = 99m,
            CategoriaId = 1,
            DataCadastro = DateTime.UtcNow
        };
        _context.Produtos.Add(produtoParaDeletar);
        await _context.SaveChangesAsync();

        // método DeleteProduto espera o parametro id
        var resultado = await _controller.DeleteProduto(produtoParaDeletar.Id);

        Assert.IsType<NoContentResult>(resultado);

        // garantir que o produto não existe mais no banco.
        var produtoDoBanco = await _context.Produtos.FindAsync(produtoParaDeletar.Id);
        Assert.Null(produtoDoBanco);
    }
}