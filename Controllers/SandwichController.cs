using Microsoft.AspNetCore.Mvc;                                         // For ControllerBase, ActionResult, routing attributes
using Microsoft.EntityFrameworkCore;                                    // For EF Core operations like FindAsync, ToListAsync
using WebAPISandwich.Model;                                             // For Sandwich and SandwichContext models

namespace WebAPISandwich.Controllers
{
    [Route("api/[controller]")]                                         // Route pattern: api/Sandwich
    [ApiController]                                                     // Enables automatic model validation & routing
    public class SandwichController : ControllerBase
    {
        private readonly SandwichContext _context;                      // DB context instance to access database

        public SandwichController(SandwichContext context)
        {
            _context = context;                                         // Dependency Injection: inject the DB context
        }

        // GET: api/Sandwich
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sandwich>>> GetSandwiches()
        {
            return await _context.Sandwiches.ToListAsync();             // Fetch all sandwiches asynchronously
        }

        // GET: api/Sandwich/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sandwich>> GetSandwich(int id)
        {
            var sandwich = await _context.Sandwiches.FindAsync(id);     // Find sandwich by ID
            if (sandwich == null) return NotFound();                    // Return 404 if not found
            return sandwich;                                            // Return sandwich object
        }

        // POST: api/Sandwich
        [HttpPost]
        public async Task<ActionResult<Sandwich>> PostSandwich(Sandwich sandwich)
        {
            _context.Sandwiches.Add(sandwich);                          // Add new sandwich to DB
            await _context.SaveChangesAsync();                          // Save changes asynchronously

            // Return 201 Created with route to new sandwich
            return CreatedAtAction(nameof(GetSandwich), new { id = sandwich.Id }, sandwich);
        }

        // PUT: api/Sandwich/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSandwich(int id, Sandwich sandwich)
        {
            if (id != sandwich.Id) return BadRequest();                 // Ensure ID in URL matches object ID
            _context.Entry(sandwich).State = EntityState.Modified;      // Mark entity as modified

            try
            {
                await _context.SaveChangesAsync();                      // Save updates
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if sandwich exists
                if (!_context.Sandwiches.Any(e => e.Id == id)) return NotFound();       
                else throw;                                             // Otherwise rethrow exception
            }

            return NoContent();                                         // 204 No Content indicates success
        }

        // DELETE: api/Sandwich/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSandwich(int id)
        {
            var sandwich = await _context.Sandwiches.FindAsync(id);     // Find sandwich by ID
            if (sandwich == null) return NotFound();                    // Return 404 if not found

            _context.Sandwiches.Remove(sandwich);                       // Remove sandwich from DB
            await _context.SaveChangesAsync();                          // Save changes

            return NoContent();                                         // 204 No Content indicates success
        }
    }
}