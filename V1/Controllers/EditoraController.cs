using AutoMapper;
using LivrariaAPI.Data.Dtos;
using LivrariaAPI.Data.EditoraRepo;
using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1.0 do Controller de editoras
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EditoraController : ControllerBase
    {
        public readonly IEditoraRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public EditoraController(IEditoraRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método que retorna todas as editoras
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParamsEditora pageParams)
        {
            var edit = await _repo.GetAllEditoraAsync(pageParams);

            var editResult = _mapper.Map<IEnumerable<EditoraDto>>(edit);

            Response.AddPagination(edit.CurrentPage, edit.PageSize,
                                    edit.TotalCount, edit.TotalPage);

            return Ok(editResult);
        }

        [HttpGet("editoras/{id}")]
        public async Task<IActionResult> GetEditoras(int id)
        {
            var edit = await _repo.GetAllLivrobyEditoraAsync(id);

            var editResult = _mapper.Map<LivroDto>(edit);

            return Ok(editResult);

        }

        /// <summary>
        /// Método que retorna apenas uma editora
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]

        public async Task<IActionResult> GetbyId(int id)
        {
            var edit = await _repo.GetEditoraByIdAsync(id);
            if (edit == null) return NotFound("Editora não foi encontrada");

            var editResult = _mapper.Map<EditoraDto>(edit);

            return Ok(editResult);
        }

        /// <summary>
        /// Metodo para inserção de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(EditoraRegisterDto model)
        {
            var edit = _mapper.Map<Editora>(model);

            if (await _repo.GetEditbyName(edit.Nome) != null)
            {
                return BadRequest(new { error = "Editora já cadastrada" });
            }

            var editAdd = await _repo.AddEditoraAsync(edit);
            if (editAdd != null)
            {
                return Created($"/api/editora/{model.Id}",
                    _mapper.Map<EditoraDto>(edit));
            }

            return BadRequest("Editora não foi criada");
        }

        /// <summary>
        /// Metodo para atualização de um dado por inteiro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditoraRegisterDto model)
        {
            var edit = await _repo.GetEditoraByIdAsync(id);
            if (edit == null) return NotFound("Editora não foi encontrada");

            if (await _repo.GetEditbyName(edit.Nome) != null)
            {
                return BadRequest(new { error = "Editora já cadastrado" });
            }

            _mapper.Map(model, edit);

            var editUpd = await _repo.UpdateEditoraAsync(edit);
            if (editUpd != null)
            {
                return Created($"/api/editora/{model.Id}",
                    _mapper.Map<EditoraDto>(edit));
            }

            return BadRequest("Editora não foi criada");
        }

        /// <summary>
        /// Método de deleta de um dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var erro = await _repo.GetAllLivrobyEditoraAsync(id);

            if (erro != null)
            {
                return BadRequest(new { error = "Ação não foi possivel pois há livro registrados com essa editora" });
            }

            var edit = await _repo.DeleteEditoraAsync(id);
            if (edit == null) return NotFound("Editora não foi encontrada");

            if (edit != null)
            {
                return Ok("Editora deletada");
            }
            return BadRequest("Editora não foi deletada");
        }
    }
}