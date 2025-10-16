using CatalogoApi.Models;
using FluentValidation;

namespace CatalogoApi.Validators
{
   public class ProdutoValidator : AbstractValidator<Produto>
   {
      public ProdutoValidator()
      {
         RuleFor(c => c.Nome)
             .NotEmpty().WithMessage("O nome do produto é obrigatório.")
             .Length(3, 100).WithMessage("O nome do produto deve ter entre 3 e 100 caracteres.");
         RuleFor(c => c.Preco)
            .NotEmpty().WithMessage("O preço do produto é obrigatório.")
            .GreaterThan(0).WithMessage("O preço deve ser maior que 0.");
         RuleFor(c => c.CategoriaId)
            .NotEmpty().WithMessage("O CategoriaId do produto é obrigatório.");
      }
   }
}