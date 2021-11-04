using AutoMapper;
using LivrariaAPI.Data.Dtos;
using LivrariaAPI.Data.UsuarioRepo;
using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1.0 do Controller de Usuários
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {

        public readonly IUsuarioRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public UsuarioController(IUsuarioRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método que retorna todos os Usuários
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParamsUsuario pageParams)
        {
            var user = await _repo.GetAllUsuarioAsync(pageParams);

            var userResult = _mapper.Map<IEnumerable<UsuarioDto>>(user);

            Response.AddPagination(user.CurrentPage, user.PageSize,
                                    user.TotalCount, user.TotalPage);

            return Ok(userResult);
        }

        /// <summary>
        /// Método que retorna apenas um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var user = await _repo.GetUsuarioByIdAsync(id);
            if (user == null) return NotFound("Usuário não encontrado");

            var userResult = _mapper.Map<UsuarioDto>(user);

            return Ok(userResult);
        }

        /// <summary>
        /// Método para inserção de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(UsuarioDtoRegister model)
        {
            var user = _mapper.Map<Usuario>(model);

            if (await _repo.GetUsuarioByEmail(user.Email) != null)
            {
                return BadRequest(new { error = "E-mail já cadastrado" });
            }

            var userAdd = await _repo.AddUsuarioAsync(user);
            if (userAdd != null)
            {
                return Created($"/api/usuario/{model.Id}",
                    _mapper.Map<UsuarioDto>(user));
            }

            return BadRequest("Usuário não foi criado");
        }

        /// <summary>
        /// Método para atualização de um dado por inteiro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioDtoRegister model) // Filtra por ID
        {
            var user = await _repo.GetUsuarioByIdAsync(id);
            if (user == null) return NotFound("Usuário não foi encontrado");

            _mapper.Map(model, user);

            if (await _repo.GetUsuarioByEmail(user.Email) != null)
            {
                return BadRequest(new { error = "E-mail já cadastrado" });
            }

            var editUpd = await _repo.UpdateUsuarioAsync(user);
            if (editUpd != null)
            {
                return Created($"/api/usuario/{model.Id}",
                    _mapper.Map<UsuarioDto>(user));
            }

            return BadRequest("Usuário não foi alterado");
        }

        /// <summary>
        /// Método que deleta um dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var erro = _repo.GetUsuariobyAluguelAsync(id);

            if (erro != null)
            {
                return BadRequest(new { error = "Ação não foi possivel pois há alugueis registrados com esse cliente" });
            }

            var user = await _repo.DeleteUsuarioAsync(id);
            if (user == null) return NotFound("Usuário não foi encontrado");

            if (user != null)
            {
                return Ok("Usuário deletado");
            }
            return BadRequest("Usuário não foi deletado");
        }
    }
}