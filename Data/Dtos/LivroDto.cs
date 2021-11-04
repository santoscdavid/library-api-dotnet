using LivrariaAPI.Models;

namespace LivrariaAPI.Data.Dtos
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public int Lancamento { get; set; }
        public int Quantidade { get; set; }
        public Editora Editora { get; set; }
    }
}