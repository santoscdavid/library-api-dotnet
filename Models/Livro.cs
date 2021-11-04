using System.Collections.Generic;

namespace LivrariaAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Livro
    {
        public Livro() { }

        public Livro(int id,
                    string nome,
                    string autor,
                    int lancamento,
                    int quantidade,
                    int editoraId)
        {
            this.Id = id;
            this.Nome = nome;
            this.Autor = autor;
            this.Lancamento = lancamento;
            this.Quantidade = quantidade;
            this.EditoraId = editoraId;
        }
        /// <summary>
        /// ID do livro (auto incremento )
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome do Livro ( Obrigatório )
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Nome do Autor do Livro ( Obrigatório )
        /// </summary>
        public string Autor { get; set; }
        /// <summary>
        /// Data de lançamento do Livro ( Obrigatório )
        /// </summary>
        public int Lancamento { get; set; }
        /// <summary>
        /// Quantidade de Livros no estoque ( Obrigatório )
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// ID da Editora do Livro ( Obrigatório )
        /// </summary>
        public int EditoraId { get; set; }
        public Editora Editora { get; set; }
        public IEnumerable<Aluguel> Alugueis { get; set; }

    }

}