using FluentValidation;
using LivrariaAPI.Data.Dtos;

namespace LivrariaAPI.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDtoRegister>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                    .WithMessage("Informe o nome do usuário")
            .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(2)
                    .WithMessage("Digite mais que 2 caracteres");
            RuleFor(u => u.Endereco)
                .NotEmpty()
                    .WithMessage("Informe o endereço do usuário")
                .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(2)
                    .WithMessage("Digite mais que 5 caracteres");
            RuleFor(u => u.Cidade)
                .NotEmpty()
                    .WithMessage("Informe o endereço do usuário")
                .MaximumLength(50)
                    .WithMessage("Digite até 50 caracteres")
                .MinimumLength(2)
                    .WithMessage("Digite mais que 5 caracteres");
            RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage("Informe um email válido");
        }
    }
}