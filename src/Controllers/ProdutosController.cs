using CatalogoApi.Data;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProdutosController(CatalogoContext context) : ControllerBase
   {
      private readonly CatalogoContext _context = context;

      // POST /api/produtos
      [HttpPost]
      public async Task<ActionResult<Produto>> PostProduto([FromBody] Produto novoProduto)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         novoProduto.DataCadastro = DateTime.Now;

         _context.Produtos.Add(novoProduto);

         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetProdutoPorId), new { id = novoProduto.Id }, novoProduto);
      }

      // GET /api/produtos
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
      {
         var produtos = await _context.Produtos.ToListAsync();

         if (produtos.Count == 0)
         {
            return NotFound("Nenhum produto encontrado.");
         }

         return Ok(produtos);
      }

      // GET /api/produtos/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Produto>> GetProdutoPorId(int id)
      {
         var produto = await _context.Produtos.FindAsync(id);

         if (produto == null)
         {
            return NotFound("Produto não encontrado.");
         }
         return Ok(produto);
      }

      // PUT /api/produtos/5
      [HttpPut("{id}")]
      public async Task<ActionResult> PutProduto(int id, [FromBody] Produto produtoAtualizado)
      {
         // verifica se o id da rota é o mesmo do objeto enviado.
         if (id != produtoAtualizado.Id)
         {
            return BadRequest("IDs inconsistentes.");
         }

         var produtoExistente = await _context.Produtos.FindAsync(id);

         if (produtoExistente == null)
         {
            return NotFound("Produto não encontrado para atualização.");
         }

         // atualiza as propriedades do produto que o EF está observando
         produtoExistente.Nome = produtoAtualizado.Nome;
         produtoExistente.Descricao = produtoAtualizado.Descricao;
         produtoExistente.Preco = produtoAtualizado.Preco;
         produtoExistente.CategoriaId = produtoAtualizado.CategoriaId;

         await _context.SaveChangesAsync();

         return NoContent();
      }

      // DELETE /api/produtos/5
      [HttpDelete("{id}")]
      public async Task<ActionResult> DeleteProduto(int id)
      {
         var produtoParaDeletar = await _context.Produtos.FindAsync(id);

         if (produtoParaDeletar == null)
         {
            return NotFound("Produto não encontrado para exclusão.");
         }

         // Marca o produto para ser removido.
         _context.Produtos.Remove(produtoParaDeletar);

         // Efetiva a exclusão no banco de dados.
         await _context.SaveChangesAsync();

         return NoContent();
      }
   }
}