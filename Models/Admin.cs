using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaAPI.Models
{
    public class Admin
    {
        public Admin() { }

        public Admin(int id, string username, string nome, string password, int ativo, string cargo, string email)
        {
            this.Id = id;
            this.Username = username;
            this.Nome = nome;
            this.Password = password;
            this.Ativo = ativo;
            this.Cargo = cargo;
            this.Email = email;

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Nome { get; set; }

        public string Password { get; set; }

        public int Ativo { get; set; }

        public string Cargo { get; set; }

        public string Email { get; set; }
    }
}