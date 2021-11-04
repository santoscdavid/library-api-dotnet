namespace LivrariaAPI.Data.Dtos
{
    public class UsuarioDtoRegister
    {
        /// <summary>
        /// Id do Usuário (Auto Incrementado)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email do Usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email do Usuário
        /// </summary>

        public string Cidade { get; set; }
        /// <summary>
        /// Endereço do Usuário
        /// </summary>
        public string Endereco { get; set; }
    }
}