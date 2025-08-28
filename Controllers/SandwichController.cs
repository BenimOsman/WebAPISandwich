using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPISandwich.Model;

namespace WebAPISandwich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandwichController : ControllerBase
    {
        private readonly SandwichContext _context;

        public SandwichController(SandwichContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sandwich>>> GetSandwiches()
        {
            return await _context.Sandwiches.ToListAsync();
        }
            
        [HttpGet("{id}")]
        public async Task<ActionResult<Sandwich>> GetSandwich(int id)
        {
            var sandwich = await _context.Sandwiches.FindAsync(id);
            if (sandwich == null) return NotFound();
            return sandwich;
        }

        [HttpPost]
        public async Task<ActionResult<Sandwich>> PostSandwich(Sandwich sandwich)
        {
            _context.Sandwiches.Add(sandwich);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSandwich), new { id = sandwich.Id }, sandwich);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSandwich(int id, Sandwich sandwich)
        {
            if (id != sandwich.Id) return BadRequest();
            _context.Entry(sandwich).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sandwiches.Any(e => e.Id == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSandwich(int id)
        {
            var sandwich = await _context.Sandwiches.FindAsync(id);
            if (sandwich == null) return NotFound();
            _context.Sandwiches.Remove(sandwich);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
