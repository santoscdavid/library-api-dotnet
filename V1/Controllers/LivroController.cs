using AutoMapper;
using LivrariaAPI.Data.Dtos;
using LivrariaAPI.Data.LivroRepo;
using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1.0 do Controller de livros
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class LivroController : ControllerBase
    {

        public readonly ILivroRepository _repo;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public LivroController(ILivroRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método que retorna todos os Livros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParamsLivro pageParams)
        {
            var livros = await _repo.GetAllLivroAsync(pageParams, true);

            var livroResult = _mapper.Map<IEnumerable<LivroDto>>(livros);

            Response.AddPagination(livros.CurrentPage, livros.PageSize,
                                    livros.TotalCount, livros.TotalPage);

            return Ok(livroResult);
        }

        /// <summary>
        /// Método que retorna um único livro   
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id) // Filtra por ID
        {
            var livro = await _repo.GetLivroByIdAsync(id, true);
            if (livro == null) return NotFound("Livro não foi encontrado"); // Verificação se existe

            var livroResult = _mapper.Map<LivroDto>(livro);

            return Ok(livroResult);
        }

        [HttpGet("GetAluguel/{id}")]
        public async Task<IActionResult> GetLivrosByAluguel(int id)
        {
            var liv = await _repo.GetLivrobyAluguelAsync(id);

            var livResult = _mapper.Map<AluguelDto>(liv);

            return Ok(livResult);
        }
        /// <summary>
        /// Método para inserção de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(LivroResgisterDto model)
        {
            var livro = _mapper.Map<Livro>(model);

            if (await _repo.GetLivrobyName(livro.Nome) != null)
            {
                return BadRequest(new { error = "Livro já cadastrado" });
            }

            var livroAdd = await _repo.AddLivroAsync(livro);
            if (livroAdd != null)
            {
                return Created($"/api/livro/{model.Id}",
                        _mapper.Map<LivroDto>(livro));
            }

            return BadRequest("Livro não foi criado");
        }

        /// <summary>
        /// Método para atualização de um dado inteira
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LivroResgisterDto model)
        {
            var livro = await _repo.GetLivroByIdAsync(id, true);
            if (livro == null) return NotFound("Livro não foi encontrado");

            _mapper.Map(model, livro);

            var livroUpd = await _repo.UpdateLivroAsync(livro);
            if (livroUpd != null)
            {
                return Created($"api/livro/{model.Id}",
                    _mapper.Map<LivroDto>(livro));
            }

            return BadRequest("Livro não foi alterado");
        }

        /// <summary>
        /// Método deleta um dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var erro = await _repo.GetLivrobyAluguelAsync(id);

            if (erro != null)
            {
                return BadRequest("Ação não foi possivel pois há alugueis registrados com esse livro");
            }

            var livro = await _repo.DeleteLivroAsync(id);
            if (livro == null) return NotFound("Livro não foi encontrado");

            if (livro != null)
            {
                return Ok("Livro deletado");
            }

            return BadRequest("Livro não foi deletado");
        }
    }
}