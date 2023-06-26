using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Registros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Context _context;

        public ClientesController(Context context)
        {
            _context = context;
        }

        private bool Existe(int ClienteId)
        {
            return (_context.Clientes?.Any(c => c.ClienteId == ClienteId)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> Obtener()
        {
            if(_context.Clientes == null)
            {
                return NotFound();
            }
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{ClienteId}")]
        public async Task<ActionResult<Clientes>> Obtener(int ClienteId)
        {
            if(_context.Clientes == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(ClienteId);

            if(clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }

        [HttpPost]
        public async Task<ActionResult<Clientes>> Crear(Clientes clientes)
        {
            if(!Existe(clientes.ClienteId))
            {
                _context.Clientes.Add(clientes);
            }
            else
            {
                _context.Clientes.Update(clientes);
            }
            await _context.SaveChangesAsync();
            return Ok(clientes);
        }

        [HttpDelete("{ClienteId}")]
        public async Task<IActionResult> Eliminar(int ClienteId)
        {
            if(_context.Clientes == null)
            {
                return NotFound();
            }
            var clientes = await _context.Clientes.FindAsync(ClienteId);
            if(clientes == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
