namespace LivrariaAPI.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            this.CurrentPage = currentPage;
            this.TotalCount = itemsPerPage;
            this.PageSize = totalItems;
            this.TotalPage = totalPages;
        }
    }
}