namespace LivrariaAPI.Helpers.PageParams
{
    public class PageParamsUsuario
    {
        public const int MaxPageSize = 50;
        /// <summary>
        /// Página atual
        /// </summary>
        public int PageNumber { get; set; } = 1;
        private int pageSize = 50;
        /// <summary>
        /// Itens por página
        /// </summary>
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        /// <summary>
        /// Filtra o usuário pelo nome
        /// </summary>
        public string Nome { get; set; } = string.Empty;
        /// <summary>
        /// Filtra o usuário pelo sobrenome
        /// </summary>
        public string Sobrenome { get; set; } = string.Empty;
        /// <summary>
        /// Filtra o usuário pelo email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Filtra o usuário pela cidade
        /// </summary>
        public string Cidade { get; set; } = string.Empty;
        /// <summary>
        /// Filtra o usuário pelo endereço
        /// </summary>
        public string Endereco { get; set; } = string.Empty;
    }
}