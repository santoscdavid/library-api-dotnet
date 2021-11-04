using FluentValidation;
using LivrariaAPI.Data.Dtos;

namespace LivrariaAPI.Validators
{
    public class LivroValidator : AbstractValidator<LivroResgisterDto>
    {
        public LivroValidator()
        {
            RuleFor(l => l.Nome)
                .NotEmpty()
                    .WithMessage("O nome do livro não pode ser vazia")
                .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(2)
                    .WithMessage("Digite mais que 3 caracteres");
            RuleFor(l => l.Autor)
                .NotEmpty()
                    .WithMessage("O nome do autor não pode ser vazia")
                .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(2)
                    .WithMessage("Digite mais que 3 caracteres");
            RuleFor(l => l.EditoraId)
                .NotEmpty()
                    .WithMessage("Informe a editora do livro");
            RuleFor(l => l.Quantidade)
                .NotEmpty()
                    .WithMessage("Informe a quantidade do livro")
                .GreaterThan(0)
                    .WithMessage("Informe um valor válido");
            RuleFor(l => l.Lancamento)
                .NotEmpty()
                    .WithMessage("Informe a data de lançamento")
                .GreaterThan(1900)
                    .WithMessage("Informe um ano maior do que 1900");
        }
    }
}