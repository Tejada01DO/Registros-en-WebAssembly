using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Registros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemasController : ControllerBase
    {
        private readonly Context _context;

        public SistemasController(Context context)
        {
            _context = context;
        }

        private bool Existe(int SistemaId)
        {
            return (_context.Sistemas?.Any(s => s.SistemaId == SistemaId)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sistemas>>> Obtener()
        {
            if(_context.Sistemas == null)
            {
                return NotFound();
            }
            return await _context.Sistemas.ToListAsync();
        }

        [HttpGet("{SistemaId}")]
        public async Task<ActionResult<Sistemas>> Obtener(int SistemaId)
        {
            if(_context.Sistemas == null)
            {
                return NotFound();
            }

            var sistemas = await _context.Sistemas.FindAsync(SistemaId);

            if(sistemas == null)
            {
                return NotFound();
            }

            return sistemas;
        }

        [HttpPost]
        public async Task<ActionResult<Sistemas>> Crear(Sistemas sistemas)
        {
            if(!Existe(sistemas.SistemaId))
            {
                _context.Sistemas.Add(sistemas);
            }
            else
            {
                _context.Sistemas.Update(sistemas);
            }
            await _context.SaveChangesAsync();
            return Ok(sistemas);
        }

        [HttpDelete("{SistemaId}")]
        public async Task<IActionResult> Eliminar(int SistemaId)
        {
            if(_context.Sistemas == null)
            {
                return NotFound();
            }
            var sistemas = await _context.Sistemas.FindAsync(SistemaId);
            if(sistemas == null)
            {
                return NotFound();
            }

            _context.Sistemas.Remove(sistemas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
