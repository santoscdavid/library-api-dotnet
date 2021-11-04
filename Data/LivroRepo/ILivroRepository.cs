using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.LivroRepo
{
    public interface ILivroRepository
    {
        Task<Livro> AddLivroAsync(Livro livro);
        Task<Livro> UpdateLivroAsync(Livro livro);
        Task<Livro> DeleteLivroAsync(int id);

        Task<PageList<Livro>> GetAllLivroAsync(PageParamsLivro pageParams, bool includeEditora);
        Task<Aluguel> GetLivrobyAluguelAsync(int livroId);
        Task<Livro> GetLivroByIdAsync(int livroId, bool IncludeEditora);
        Task<Livro> GetLivrobyName(string nome);
    }
}