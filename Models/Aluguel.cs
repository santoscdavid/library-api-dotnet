using System;
namespace LivrariaAPI.Models
{
    public class Aluguel
    {
        public Aluguel() { }

        public Aluguel(int id,
                    int livroId,
                    int usuarioId,
                    DateTime aluguelFeito,
                    DateTime previsaoEntrega,
                    DateTime devolucao)
        {
            this.Id = id;
            this.LivroId = livroId;
            this.UsuarioId = usuarioId;
            this.PrevisaoEntrega = previsaoEntrega;
        }
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime AluguelFeito { get; set; } = DateTime.Today;
        public DateTime PrevisaoEntrega { get; set; }
        public DateTime? Devolucao { get; set; } = null;
    }
}