using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.AlguelRepo
{
    public interface IAluguelRepository
    {
        Task<Aluguel> AddAluguelAsync(Aluguel aluguel);
        Task<Aluguel> UpdateAluguelAsync(Aluguel aluguel);
        Task<Aluguel> DeleteAluguelAsync(int id);
        Task<PageList<Aluguel>> GetAllAluguelAsync(PageParamsAluguel pageParams, bool Include = false);
        Task<PageList<Aluguel>> GetLastForAluguelAsync(PageParamsAluguel pageParams, bool Include = false);
        Task<Aluguel> GetAluguelByIdAsync(int aluguelId, bool Include = false);
    }
}