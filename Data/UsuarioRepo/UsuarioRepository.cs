using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.UsuarioRepo
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            var user = await _context.Usuarios
                                     .FirstOrDefaultAsync(u => u.Id == usuario.Id);
            if (user == null)
            {
                return null;
            }
            _context.Entry(user).CurrentValues.SetValues(usuario);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<Usuario> DeleteUsuarioAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            var usuarioRemoved = _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return usuarioRemoved.Entity;
        }

        public async Task<PageList<Usuario>> GetAllUsuarioAsync(PageParamsUsuario pageParams)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.AsNoTracking().OrderBy(u => u.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
            {
                query = query.Where(usuario => usuario.Nome
                                                      .ToUpper()
                                                      .Contains(pageParams.Nome.ToUpper())
                                    );
            }
            if (!string.IsNullOrEmpty(pageParams.Email))
            {
                query = query.Where(usuario => usuario.Email
                                                      .ToUpper()
                                                      .Contains(pageParams.Email.ToUpper())
                                    );
            }
            if (!string.IsNullOrEmpty(pageParams.Cidade))
            {
                query = query.Where(usuario => usuario.Cidade
                                                      .ToUpper()
                                                      .Contains(pageParams.Cidade.ToUpper())
                                    );
            }
            if (!string.IsNullOrEmpty(pageParams.Endereco))
            {
                query = query.Where(usuario => usuario.Endereco
                                                      .ToUpper()
                                                      .Contains(pageParams.Endereco.ToUpper())
                                    );
            }

            return await PageList<Usuario>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarioByAluguelIdAsync(int usuarioId, bool includeAluguel)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            if (includeAluguel)
            {
                query = query.Include(u => u.Alugueis)
                                .ThenInclude(a => a.Livro)
                                .ThenInclude(l => l.Editora);
            }

            query = query.AsNoTracking()
                            .OrderBy(u => u.Id)
                            .Where(u => u.Alugueis.Any(
                                a => a.UsuarioId == usuarioId
                            ));

            return await query.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int usuarioId)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.AsNoTracking()
                        .OrderBy(u => u.Id)
                        .Where(user => user.Id == usuarioId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Aluguel> GetUsuariobyAluguelAsync(int userId)
        {
            IQueryable<Aluguel> query = _context.Alugueis;

            query = query.AsNoTracking().OrderBy(l => l.Id)
                                        .Where(l => l.UsuarioId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            Usuario user = new Usuario();

            user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
    }
}