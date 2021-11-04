using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.UsuarioRepo
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario);
        Task<Usuario> DeleteUsuarioAsync(int id);
        Task<PageList<Usuario>> GetAllUsuarioAsync(PageParamsUsuario pageParams);
        Task<IEnumerable<Usuario>> GetAllUsuarioByAluguelIdAsync(int usuarioId, bool includeAluguel);
        Task<Aluguel> GetUsuariobyAluguelAsync(int livroId);
        Task<Usuario> GetUsuarioByIdAsync(int usuarioId);
        Task<Usuario> GetUsuarioByEmail(string email);
    }
}