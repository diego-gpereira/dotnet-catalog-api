using CatalogoApi.Controllers;
using CatalogoApi.Data;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CatalogApi.Tests
{
    public class ProdutosControllerTests
    {
        [Fact] // A etiqueta do xUnit que marca este método como um teste
        public async Task PostProduto_QuandoDadosValidos_DeveRetornar201Created()
        {
            // --- ARRANGE (Preparar o Cenário) ---

            // 1. Configurar um banco de dados em memória exclusivo para este teste
            var options = new DbContextOptionsBuilder<CatalogoContext>()
                .UseInMemoryDatabase(databaseName: "Teste_CriacaoProdutoComSucesso")
                .Options;

            // 2. Criar instâncias do nosso contexto e do nosso controller
            //    O 'using' garante que o contexto será "limpo" da memória após o teste.
            using (var context = new CatalogoContext(options))
            {
                var controller = new ProdutosController(context);

                // 3. Criar o objeto que vamos enviar para a API
                var novoProduto = new Produto
                {
                    Nome = "Produto de Teste",
                    Descricao = "Descrição Válida",
                    Preco = 10.50m,
                    CategoriaId = 1
                };

                // --- ACT (Agir / Executar a Ação) ---

                // 4. Chamar o método que queremos testar
                var resultado = await controller.PostProduto(novoProduto);

                // --- ASSERT (Verificar o Resultado) ---

                // 5. Verificar se o resultado da ação é do tipo esperado (CreatedAtActionResult)
                var actionResult = Assert.IsType<CreatedAtActionResult>(resultado.Result);

                // 6. Verificar se o objeto retornado no corpo da resposta é do tipo Produto
                var produtoRetornado = Assert.IsType<Produto>(actionResult.Value);

                // 7. Verificar se o nome do produto retornado é o mesmo que enviamos
                Assert.Equal("Produto de Teste", produtoRetornado.Nome);

                // 8. Verificar se o produto foi realmente salvo no nosso banco em memória
                Assert.Equal(1, context.Produtos.Count());
            }
        }
    }
}