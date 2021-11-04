using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.LivroRepo
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DataContext _context;
        public LivroRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Livro> AddLivroAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> UpdateLivroAsync(Livro livro)
        {
            var liv = await _context.Livros
                                    .FirstOrDefaultAsync(l => l.Id == livro.Id);
            if (liv == null)
            {
                return null;
            }
            _context.Entry(liv).CurrentValues.SetValues(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> DeleteLivroAsync(int id)
        {
            var liv = await _context.Livros.FindAsync(id);
            if (liv == null)
            {
                return null;
            }
            var livroRemoved = _context.Livros.Remove(liv);
            await _context.SaveChangesAsync();
            return livroRemoved.Entity;
        }

        public async Task<PageList<Livro>> GetAllLivroAsync(PageParamsLivro pageParams, bool IncludeEditora)
        {
            IQueryable<Livro> query = _context.Livros;

            if (IncludeEditora)
            {
                query = query.Include(a => a.Editora);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(livro => livro.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper())
                                    );
            if (!string.IsNullOrEmpty(pageParams.Autor))
                query = query.Where(livro => livro.Autor
                                                  .ToUpper()
                                                  .Contains(pageParams.Autor.ToUpper())
                                    );
            if (pageParams.Lancamento > 0)
                query = query.Where(livro => livro.Lancamento == pageParams.Lancamento);

            return await PageList<Livro>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Aluguel> GetLivrobyAluguelAsync(int userId)
        {
            IQueryable<Aluguel> query = _context.Alugueis;

            query = query.AsNoTracking().OrderBy(l => l.Id)
                                        .Where(l => l.LivroId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Livro> GetLivroByIdAsync(int livroId, bool IncludeEditora)
        {
            IQueryable<Livro> query = _context.Livros;

            if (IncludeEditora)
            {
                query = query.Include(l => l.Editora);
            }

            query = query.AsNoTracking()
                                .OrderBy(l => l.Id)
                                .Where(livro => livro.Id == livroId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Livro> GetLivrobyName(string nome)
        {
            Livro liv = new Livro();

            liv = await _context.Livros.FirstOrDefaultAsync(l => l.Nome == nome);

            return liv;
        }
    }
}