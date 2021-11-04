namespace LivrariaAPI.Helpers.PageParams
{
    public class PageParamsLivro
    {
        public const int MaxPageSize = 50;
        /// <summary>
        /// Página Atual
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
        /// Filtra o livro pelo nome
        /// </summary>
        public string Nome { get; set; } = string.Empty;
        /// <summary>
        /// Filtra o livro pelo Autor
        /// </summary>
        public string Autor { get; set; } = string.Empty;
        /// <summary>
        /// Filtra o livro pelo lançamento
        /// </summary>
        public int? Lancamento { get; set; } = null;
    }
}