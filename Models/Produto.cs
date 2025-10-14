using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoApi.Models;

public class Produto
{
   [Key]
   public int Id { get; set; }

   [Required]
   [MaxLength(120)]
   public required string Nome { get; set; }

   [MaxLength(500)]
   public string? Descricao { get; set; }

   [Required]
   [Column(TypeName = "decimal(18,2)")]
   public required decimal Preco { get; set; }

   public DateTime DataCadastro { get; set; }

   public int CategoriaId { get; set; }

   public Categoria? Categoria { get; set; } // todo produto está ligado a um objeto categoria
}