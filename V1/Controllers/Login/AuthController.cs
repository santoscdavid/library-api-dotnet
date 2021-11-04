using LivrariaAPI.Data;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.V1.Controllers.Login
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }


        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.Id }, admin);
        }
    }
}