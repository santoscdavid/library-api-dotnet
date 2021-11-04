namespace LivrariaAPI.Data.Dtos
{
    /// <summary>
    /// DTO de Editora para Registrar
    /// </summary>
    public class EditoraRegisterDto
    {
        /// <summary>
        /// ID do Editora (auto incremento )
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome da Editora
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cidade da Editora
        /// </summary>
        public string Cidade { get; set; }
    }
}