using System.Collections.Generic;

namespace LivrariaAPI.Models
{
    public class Usuario
    {
        public Usuario() { }

        public Usuario(int id,
                    string nome,
                    string email,
                    string cidade,
                    string endereco)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Cidade = cidade;
            this.Endereco = endereco;

        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }

        public IEnumerable<Aluguel> Alugueis { get; set; }
    }
}