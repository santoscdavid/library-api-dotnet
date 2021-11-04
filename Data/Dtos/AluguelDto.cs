using LivrariaAPI.Models;
using System;

namespace LivrariaAPI.Data.Dtos
{
    public class AluguelDto
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime? AluguelFeito { get; set; } = DateTime.Today;
        public DateTime PrevisaoEntrega { get; set; }
        public DateTime? Devolucao { get; set; } = null;
    }
}