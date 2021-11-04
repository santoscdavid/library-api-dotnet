using System;

namespace LivrariaAPI.Data.Dtos
{
    public class AluguelDtoRegister
    {
        /// <summary>
        /// Id do aluguel (Auto Incrementado)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id do livro alugado
        /// </summary>
        public int LivroId { get; set; }
        /// <summary>
        /// Id do Cliente que realizou o aluguel
        /// </summary>
        public int UsuarioId { get; set; }
        /// <summary>
        /// Dia em que o Aluguel é feito (Auto Incrementado)
        /// </summary>
        public DateTime? AluguelFeito { get; set; } = DateTime.Today;
        /// <summary>
        /// Data prevista da devolução
        /// </summary>
        public DateTime PrevisaoEntrega { get; set; }
        /// <summary>
        /// Data da devolução
        /// </summary>
        public DateTime? Devolucao { get; set; } = null;
    }
}