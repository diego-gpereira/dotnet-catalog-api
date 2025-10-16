using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace CatalogoApi.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Nome { get; set; }

        [JsonIgnore] // Ignora esta propriedade ao serializar para evitar loops infinitos => os produtos retornam o Json Categoria
        public ICollection<Produto>? Produtos { get; set; } // uma categoria pode ter uma coleção de produtos.
    }
}