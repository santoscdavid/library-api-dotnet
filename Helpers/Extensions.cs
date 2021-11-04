using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LivrariaAPI.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse respose,
            int currentPage,
            int itemsPerPage,
            int totalItems,
            int totalPages
        )
        {
            var paginationHeader = new PaginationHeader(
                    currentPage, itemsPerPage, totalItems, totalPages
            );

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            respose.Headers.Add("Pagination", JsonConvert.SerializeObject(
                paginationHeader, camelCaseFormatter));

            respose.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
    }
}