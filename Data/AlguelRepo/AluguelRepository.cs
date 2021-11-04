using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.AlguelRepo
{
    public class AluguelRepository : IAluguelRepository
    {
        private readonly DataContext _context;
        public AluguelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Aluguel> AddAluguelAsync(Aluguel aluguel)
        {
            await _context.Alugueis.AddAsync(aluguel);
            await _context.SaveChangesAsync();
            return aluguel;
        }
        public async Task<Aluguel> UpdateAluguelAsync(Aluguel aluguel)
        {
            var alu = await _context.Alugueis
                                    .FirstOrDefaultAsync(a => a.Id == aluguel.Id);
            if (alu == null)
            {
                return null;
            }
            _context.Entry(alu).CurrentValues.SetValues(aluguel);
            await _context.SaveChangesAsync();
            return aluguel;
        }
        public async Task<Aluguel> DeleteAluguelAsync(int id)
        {
            var alu = await _context.Alugueis.FindAsync(id);
            if (alu == null)
            {
                return null;
            }
            var aluguelRemoved = _context.Alugueis.Remove(alu);
            await _context.SaveChangesAsync();
            return aluguelRemoved.Entity;
        }

        public async Task<PageList<Aluguel>> GetAllAluguelAsync(PageParamsAluguel pageParams, bool Include = true)
        {
            IQueryable<Aluguel> query = _context.Alugueis;

            if (Include)
            {
                query = query.Include(a => a.Usuario)
                            .Include(a => a.Livro);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await PageList<Aluguel>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<PageList<Aluguel>> GetLastForAluguelAsync(PageParamsAluguel pageParams, bool Include = false)
        {
            IQueryable<Aluguel> query = _context.Alugueis;

            if (Include)
            {
                query = query.Include(a => a.Usuario)
                            .Include(a => a.Livro);
            }

            query = query.AsNoTracking().OrderByDescending(a => a.Id);

            return await PageList<Aluguel>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Aluguel> GetAluguelByIdAsync(int aluguelId, bool Include = false)
        {
            IQueryable<Aluguel> query = _context.Alugueis;

            if (Include)
            {
                query = query.Include(a => a.Usuario)
                            .Include(a => a.Livro);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(a => a.Id == aluguelId);

            return await query.FirstOrDefaultAsync();
        }
    }
}