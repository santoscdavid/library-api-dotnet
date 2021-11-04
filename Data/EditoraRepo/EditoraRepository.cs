using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.Data.EditoraRepo
{
    /// <summary>
    /// Repositório de Editora
    /// </summary>
    public class EditoraRepository : IEditoraRepository
    {
        private readonly DataContext _context;
        public EditoraRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Editora> AddEditoraAsync(Editora editora)
        {
            await _context.Editoras.AddAsync(editora);
            await _context.SaveChangesAsync();
            return editora;
        }
        public async Task<Editora> UpdateEditoraAsync(Editora editora)
        {
            var editoraConsultado = await _context.Editoras
                                                 .FirstOrDefaultAsync(e => e.Id == editora.Id);
            if (editoraConsultado == null)
            {
                return null;
            }
            _context.Entry(editoraConsultado).CurrentValues.SetValues(editora);
            await _context.SaveChangesAsync();
            return editoraConsultado;
        }
        public async Task<Editora> DeleteEditoraAsync(int id)
        {
            var editoraConsultado = await _context.Editoras.FindAsync(id);
            if (editoraConsultado == null)
            {
                return null;
            }
            var editoraRemovido = _context.Editoras.Remove(editoraConsultado);
            await _context.SaveChangesAsync();
            return editoraRemovido.Entity;
        }


        public async Task<PageList<Editora>> GetAllEditoraAsync(PageParamsEditora pageParams)
        {
            IQueryable<Editora> query = _context.Editoras;

            query = query.AsNoTracking().OrderBy(e => e.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(editora => editora.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper())
                                    );
            if (!string.IsNullOrEmpty(pageParams.Cidade))
                query = query.Where(editora => editora.Cidade
                                                  .ToUpper()
                                                  .Contains(pageParams.Cidade.ToUpper())
                                    );

            return await PageList<Editora>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
        public async Task<Editora> GetEditoraByIdAsync(int editoraId)
        {
            IQueryable<Editora> query = _context.Editoras;

            query = query.AsNoTracking()
                        .OrderBy(e => e.Id)
                        .Where(edit => edit.Id == editoraId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Livro> GetAllLivrobyEditoraAsync(int editoraId)
        {
            IQueryable<Livro> query = _context.Livros;

            query = query.AsNoTracking().OrderBy(l => l.Id)
                                        .Where(l => l.EditoraId == editoraId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Editora> GetEditbyName(string nome)
        {
            Editora edit = new Editora();

            edit = await _context.Editoras.FirstOrDefaultAsync(e => e.Nome == nome);

            return edit;
        }
    }
}