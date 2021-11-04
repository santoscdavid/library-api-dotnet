using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.EditoraRepo
{
    public interface IEditoraRepository
    {
        Task<Editora> AddEditoraAsync(Editora editora);
        Task<Editora> UpdateEditoraAsync(Editora editora);
        Task<Editora> DeleteEditoraAsync(int id);

        Task<PageList<Editora>> GetAllEditoraAsync(PageParamsEditora pageParams);
        Task<Livro> GetAllLivrobyEditoraAsync(int editoraId);
        Task<Editora> GetEditoraByIdAsync(int editoraId);
        Task<Editora> GetEditbyName(string nome);
    }
}