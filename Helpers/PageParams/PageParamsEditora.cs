namespace LivrariaAPI.Helpers.PageParams
{
    public class PageParamsEditora
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
        /// Filtra a editora pelo nome
        /// </summary>
        public string Nome { get; set; } = string.Empty;
        /// <summary>
        /// Filtra a editora pela cidade
        /// </summary>
        public string Cidade { get; set; } = string.Empty;
    }
}