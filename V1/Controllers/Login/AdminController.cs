using LivrariaAPI.Data;
using LivrariaAPI.Models;
using LivrariaAPI.Services;
using LivrariaAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LivrariaAPI.V1.Controllers.Login
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        public AdminController(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Autenticar([FromBody] Admin admin)
        {
            Admin adm = new Admin();
            adm = _authService.Login(admin.Username, admin.Password);
            if (adm == null)
            {
                return BadRequest(new { error = "Usuário ou senha inválidos" });
            }

            if (adm.Ativo == 0)
            {
                return BadRequest(new { error = "Usuário Inativo !" });
            }

            var token = TokenService.GenerateToken(adm);


            adm.Password = "";
            return new { adm = adm, token = token };

        }

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Cadastrar([FromBody] Admin admin)
        {
            Admin adm = new Admin();
            if (admin.Username != null && admin.Password != null && admin.Nome != null)
            {
                if (admin.Password.Length < 5)
                {
                    return BadRequest(new { error = "A Senha precisa conter mais de 5 caracteres" });
                }
                if (admin.Username.Length < 3)
                {
                    return BadRequest(new { error = "O nome de usuário precisa conter mais de 3 caracteres" });
                }
                if (admin.Nome == "")
                {
                    return BadRequest(new { error = "O Nome não pode ser nulo" });
                }
                if (_authService.GetAdmin(admin.Username) != null)
                {
                    return BadRequest(new { error = "Nome de usuário já cadastrado" });
                }
                if (_authService.GetAdminByEmail(admin.Email) != null)
                {
                    return BadRequest(new { error = "E-mail já cadastrado" });
                }
                admin = await _authService.Cadastrar(admin);
            }
            else
            {
                return BadRequest(new { error = "Dados para o cadastro inválidos !" });
            }

            if (admin == null)
            {
                return BadRequest(new { error = "Não foi possivel cadastrar o usuário" });
            }

            return (new { message = "Usuário cadastrado com sucesso !" });

        }
        [HttpPut]
        [Route("admin/editar")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<dynamic>> EditarAdminAdm(int id, Admin adminEditado)
        {

            Admin admin = new Admin();
            admin = await _authService.PutAdminAdm(id, adminEditado);
            if (admin == null)
            {
                return BadRequest(new { error = "Falha ao editar usuário" });
            }

            return (new { message = "Usuário editado com sucesso !" });
        }
        [HttpPut]
        [Route("editar")]
        [Authorize]
        public async Task<ActionResult<dynamic>> EditarAdmin(int id, Admin adminEditado)
        {

            Admin admin = new Admin();
            admin = await _authService.PutAdmin(id, adminEditado);
            if (admin == null)
            {
                return BadRequest(new { error = "Falha ao editar usuário" });
            }

            return (new { message = "Usuário editado com sucesso !" });
        }
        [HttpDelete]
        [Route("admin/deletar")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<dynamic>> DeleteAdminAdm(int id)
        {

            bool usuario = await _authService.DeleteAdmin(id);
            if (usuario == false)
            {
                return BadRequest(new { error = "Falha ao deletar usuário" });
            }

            return (new { message = "Usuário deletado com sucesso !" });
        }


        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Autenticado() => String.Format("Autenticado: {0}", User.Identity.Name);

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonimo() => "Anônimo";

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Admin() => "Administrador";
    }
}
