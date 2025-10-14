using CatalogoApi.Models;
using FluentValidation;

namespace CatalogoApi.Validators
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .Length(3, 100).WithMessage("O nome da categoria deve ter entre 3 e 100 caracteres.");
        }
    }
}