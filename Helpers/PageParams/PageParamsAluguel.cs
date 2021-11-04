namespace LivrariaAPI.Helpers.PageParams
{
    public class PageParamsAluguel
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

        // public string AluguelFeito { get; set; } = string.Empty;
        // public string PrevisaoEntrega { get; set; } = string.Empty; 
        // public string Devolucao { get; set; } = string.Empty;
    }
}