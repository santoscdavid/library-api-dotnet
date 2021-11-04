namespace LivrariaAPI.Data.Dtos
{
    /// <summary>
    /// DTO de Livro para Registrar
    /// </summary>
    public class LivroResgisterDto
    {
        /// <summary>
        /// ID do livro (auto incrementado )
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome do Livro ( Obrigat�rio )
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Nome do Autor do Livro ( Obrigat�rio )
        /// </summary>
        public string Autor { get; set; }
        /// <summary>
        /// Data de lan�amento do Livro ( Obrigat�rio )
        /// </summary>
        public int Lancamento { get; set; }
        /// <summary>
        /// Quantidade de Livros no estoque ( Obrigat�rio )
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// ID da Editora do Livro ( Obrigat�rio )
        /// </summary>
        public int EditoraId { get; set; }
    }
}