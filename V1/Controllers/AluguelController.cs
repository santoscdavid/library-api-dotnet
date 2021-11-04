using AutoMapper;
using LivrariaAPI.Data.AlguelRepo;
using LivrariaAPI.Data.Dtos;
using LivrariaAPI.Helpers;
using LivrariaAPI.Helpers.PageParams;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1.0 do Controller de Algueis
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AluguelController : ControllerBase
    {
        private readonly IAluguelRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AluguelController(IAluguelRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método que retorna todos os alugueis
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParamsAluguel pageParams)
        {
            var a = await _repo.GetAllAluguelAsync(pageParams, true);

            var aResult = _mapper.Map<IEnumerable<AluguelDto>>(a);

            var total = aResult.Count();

            Response.AddPagination(a.CurrentPage, a.PageSize,
                                    a.TotalCount, a.TotalPage);

            return Ok(aResult);
        }

        /// <summary>
        /// Método que retorna todos alugueis em ordem decrescente
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet("LastAluguel")]
        public async Task<IActionResult> GetLast([FromQuery] PageParamsAluguel pageParams)
        {
            var a = await _repo.GetLastForAluguelAsync(pageParams, true);

            var aResult = _mapper.Map<IEnumerable<AluguelDto>>(a);

            Response.AddPagination(a.CurrentPage, a.PageSize,
                                    a.TotalCount, a.TotalPage);
            return Ok(aResult);
        }

        /// <summary>
        /// Método que retorna apenas um aluguel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var a = await _repo.GetAluguelByIdAsync(id);
            if (a == null) return NotFound("Aluguel não foi encontrado");

            var aResult = _mapper.Map<AluguelDto>(a);

            return Ok(aResult);
        }

        /// <summary>
        /// Método para inserção de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(AluguelDtoRegister model)
        {
            var a = _mapper.Map<Aluguel>(model);

            var aluguelAdd = await _repo.AddAluguelAsync(a);
            if (aluguelAdd != null)
            {
                return Created($"/api/aluguel/{model.Id}",
                        _mapper.Map<AluguelDto>(a));
            }

            return BadRequest("Aluguel não foi criado");
        }

        /// <summary>
        /// Método para atualização de um dado por inteiro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AluguelDtoRegister model)
        {
            var a = await _repo.GetAluguelByIdAsync(id, true);
            if (a == null) return NotFound("Aluguel não foi encontrado");

            _mapper.Map(model, a);

            var aluguelUpd = await _repo.UpdateAluguelAsync(a);
            if (aluguelUpd != null)
            {
                return Created($"/api/aluguel/{model.Id}",
                _mapper.Map<AluguelDto>(a));
            }

            return BadRequest("Aluguel não foi alterada");
        }

        /// <summary>
        /// Método que deleta um dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _repo.DeleteAluguelAsync(id);
            if (a == null) return NotFound("Aluguel não foi encontrado");

            if (a != null)
            {
                return Ok("Usuário deletado");
            }

            return BadRequest("Aluguel não foi deletado");
        }
    }
}