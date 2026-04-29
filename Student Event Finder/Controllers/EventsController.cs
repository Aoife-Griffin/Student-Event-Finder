using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Event_Finder.Data;
using Student_Event_Finder.Models;
using System.Linq;

namespace Student_Event_Finder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/events/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return eventItem;
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByCategory(string category)
        {
            var events = await _context.Events
                .Where(e => e.Category.ToLower() == category.ToLower())
                .ToListAsync();

            if (!events.Any())
                {
                return NotFound();
                }

            return events;
        }

        [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<Event>>> GetUpcomingEvents()
        {
            var upcomingEvents = await _context.Events
                .Where(e => e.Date >= DateTime.Now)
                .OrderBy(e => e.Date)
                .ToListAsync();

            if (!upcomingEvents.Any())
            {
                return NotFound();
            }

            return upcomingEvents;
        }

        [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByDate(DateTime date)
    {
            var events = await _context.Events
                .Where(e => e.Date.Date == date.Date)
                .OrderBy(e => e.Date)
                .ToListAsync();

            if (!events.Any())
            {
                return NotFound();
            }

            return events;
}

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, Event eventItem)
{
        if (id != eventItem.EventId)
        {
            return BadRequest();
        }

        _context.Entry(eventItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Events.Any(e => e.EventId == id))
            {
                return NotFound();
            }
            
            else
            {
            throw;
            }
        }

        return NoContent();
}

        // POST: api/events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event eventItem)
        {
            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = eventItem.EventId }, eventItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();

        return NoContent();
        }
    }
}