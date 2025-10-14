using CatalogoApi.Data;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class CategoriasController(CatalogoContext context) : ControllerBase
   {
      private readonly CatalogoContext _context = context;

      // GET /api/categorias
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
      {
         // 'await' e '.ToListAsync()' para buscar os dados do banco de forma assíncrona
         var categorias = await _context.Categorias.ToListAsync();

         if (categorias == null || !categorias.Any())
         {
            return NotFound("Nenhuma categoria encontrada.");
         }
         return Ok(categorias);
      }

      // POST /api/categorias
      [HttpPost]
      public async Task<ActionResult<Categoria>> PostCategoria([FromBody] Categoria novaCategoria)
      {
         _context.Categorias.Add(novaCategoria);

         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetCategoriaPorId), new { id = novaCategoria.Id }, novaCategoria);
      }

      // GET /api/categorias/1 
      [HttpGet("{id}")]
      public async Task<ActionResult<Categoria>> GetCategoriaPorId(int id)
      {
         // 'FindAsync(id)' é a forma otimizada de buscar um item pela sua chave primária
         var categoria = await _context.Categorias.FindAsync(id);

         if (categoria == null)
         {
            return NotFound("Categoria não encontrada.");
         }

         return Ok(categoria);
      }
   }
}