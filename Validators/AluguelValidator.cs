using FluentValidation;
using LivrariaAPI.Data.Dtos;
using System;

namespace LivrariaAPI.Validators
{
    public class AluguelValidator : AbstractValidator<AluguelDtoRegister>
    {
        public AluguelValidator()
        {
            RuleFor(a => a.UsuarioId)
                .NotEmpty()
                    .WithMessage("Informe qual o usuário")
                .GreaterThan(0)
                    .WithMessage("Informe um valor válido");
            RuleFor(a => a.LivroId)
                .NotEmpty()
                    .WithMessage("Informe qual o livro")
                .GreaterThan(0)
                    .WithMessage("Informe um valor válido");
            RuleFor(a => a.AluguelFeito)
                .NotEmpty()
                    .WithMessage("Informe qual a data prevista para a devolução")
                .LessThanOrEqualTo(DateTime.Today)
                    .WithMessage("A data em que o aluguel foi feito não pode ser maior que a de hoje");
            RuleFor(a => a.PrevisaoEntrega)
                .NotEmpty()
                    .WithMessage("Informe qual a data prevista para a devolução")
                .GreaterThan(DateTime.Today)
                    .WithMessage("A data em que a devolução foi marcada não pode ser maior que a de hoje");
        }
    }
}