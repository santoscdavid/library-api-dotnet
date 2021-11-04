using FluentValidation;
using LivrariaAPI.Data.Dtos;

namespace LivrariaAPI.Validators
{
    public class EditoraValidator : AbstractValidator<EditoraRegisterDto>
    {
        public EditoraValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty()
                    .WithMessage("O nome da editora não pode ser vazia")
                .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(2)
                    .WithMessage("Digite mais que 2 caracteres");

            RuleFor(e => e.Cidade)
                .NotEmpty()
                    .WithMessage("A cidade da editora não pode ser vazia")
                .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(3)
                    .WithMessage("Digite mais que 3 caracteres");
        }
    }
}