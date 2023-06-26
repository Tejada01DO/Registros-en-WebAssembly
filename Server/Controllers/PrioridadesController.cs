using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Registros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioridadesController : ControllerBase
    {
        private readonly Context _context;

        public PrioridadesController(Context context)
        {
            _context = context;
        }

        private bool Existe(int PrioridadId)
        {
            return (_context.Prioridades?.Any(p => p.PrioridadId == PrioridadId)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prioridades>>> Obtener()
        {
            if(_context.Prioridades == null)
            {
                return NotFound();
            }
            return await _context.Prioridades.ToListAsync();
        }

        [HttpGet("{PrioridadId}")]
        public async Task<ActionResult<Prioridades>> Obtener(int PrioridadId)
        {
            if(_context.Prioridades == null)
            {
                return NotFound();
            }

            var prioridades = await _context.Prioridades.FindAsync(PrioridadId);

            if(prioridades == null)
            {
                return NotFound();
            }

            return prioridades;
        }

        [HttpPost]
        public async Task<ActionResult<Prioridades>> Crear(Prioridades prioridades)
        {
            if(!Existe(prioridades.PrioridadId))
            {
                _context.Prioridades.Add(prioridades);
            }
            else
            {
                _context.Prioridades.Update(prioridades);
            }
            await _context.SaveChangesAsync();
            return Ok(prioridades);
        }

        [HttpDelete("{PrioridadId}")]
        public async Task<IActionResult> Eliminar(int PrioridadId)
        {
            if(_context.Prioridades == null)
            {
                return NotFound();
            }
            var prioridades = await _context.Prioridades.FindAsync(PrioridadId);
            if(prioridades == null)
            {
                return NotFound();
            }

            _context.Prioridades.Remove(prioridades);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
