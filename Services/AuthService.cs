using LivrariaAPI.Data;
using LivrariaAPI.Models;
using LivrariaAPI.Services.Interface;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }

        public async Task<Admin> Cadastrar(Admin admin)
        {
            try
            {
                var password = sha256_hash(admin.Password);
                admin.Password = password;
                admin.Cargo = "admin";
                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();

                return admin;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                admin = new Admin();
            }
            return admin;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return false;
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return true;
        }

        public Admin GetAdmin(string username)
        {
            Admin admin = new Admin();
            admin = _context.Admins.FirstOrDefault(a => a.Username == username);

            return admin;
        }

        public Admin GetAdminByEmail(string email)
        {
            Admin admin = new Admin();
            admin = _context.Admins.FirstOrDefault(u => u.Email == email);

            return admin;
        }

        public async Task<Admin> GetAdminById(int id)
        {
            Admin admin = new Admin();
            admin = await _context.Admins.FindAsync(id);

            return admin;
        }

        public Admin Login(string username, string password)
        {
            Admin admin = new Admin();
            var senha = sha256_hash(password);
            try
            {
                admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                admin = new Admin();
            }
            return admin;
        }

        public async Task<Admin> PutAdmin(int id, Admin adminEditado)
        {
            Admin admin = new Admin();

            try
            {
                admin = await _context.Admins.FindAsync(id);
                if (adminEditado.Password != "" && adminEditado.Password != null)
                {
                    var password = sha256_hash(adminEditado.Password);
                    admin.Password = password;
                }
                admin.Nome = adminEditado.Nome;
                admin.Username = adminEditado.Username;
                admin.Email = adminEditado.Email;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                admin = new Admin();
            }

            return admin;
        }

        public async Task<Admin> PutAdminAdm(int id, Admin adminEditado)
        {
            Admin admin = new Admin();

            try
            {
                admin = await _context.Admins.FindAsync(id);
                if (adminEditado.Password != "" && adminEditado.Password != null)
                {
                    var password = sha256_hash(adminEditado.Password);
                    admin.Password = password;
                }
                admin.Nome = adminEditado.Nome;
                admin.Username = adminEditado.Username;
                admin.Ativo = adminEditado.Ativo;
                admin.Cargo = adminEditado.Cargo;
                admin.Email = adminEditado.Email;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                admin = new Admin();
            }

            return admin;
        }

        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}
