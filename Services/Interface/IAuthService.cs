using LivrariaAPI.Models;
using System.Threading.Tasks;

namespace LivrariaAPI.Services.Interface
{
    public interface IAuthService
    {
        Admin Login(string username, string password);
        Task<Admin> Cadastrar(Admin admin);
        Admin GetAdmin(string username);
        Admin GetAdminByEmail(string email);
        Task<Admin> GetAdminById(int id);
        Task<Admin> PutAdmin(int id, Admin adminEditado);
        Task<bool> DeleteAdmin(int id);
        Task<Admin> PutAdminAdm(int id, Admin adminEditado);
    }
}
