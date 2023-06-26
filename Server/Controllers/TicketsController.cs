using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Registros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly Context _context;

        public TicketsController(Context context)
        {
            _context = context;
        }

        private bool Existe(int TicketId)
        {
            return (_context.Tickets?.Any(t => t.TicketId == TicketId)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tickets>>> Obtener()
        {
            if(_context.Tickets == null)
            {
                return NotFound();
            }
            return await _context.Tickets.ToListAsync();
        }

        [HttpGet("{TicketId}")]
        public async Task<ActionResult<Tickets>> Obtener(int TicketId)
        {
            if(_context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.FindAsync(TicketId);

            if(tickets == null)
            {
                return NotFound();
            }

            return tickets;
        }

        [HttpPost]
        public async Task<ActionResult<Tickets>> Crear(Tickets tickets)
        {
            if(!Existe(tickets.TicketId))
            {
                _context.Tickets.Add(tickets);
            }
            else
            {
                _context.Tickets.Update(tickets);
            }
            await _context.SaveChangesAsync();
            return Ok(tickets);
        }

        [HttpDelete("{TicketId}")]
        public async Task<IActionResult> Eliminar(int TicketId)
        {
            if(_context.Tickets == null)
            {
                return NotFound();
            }
            var tickets = await _context.Tickets.FindAsync(TicketId);
            if(tickets == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(tickets);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
