using BoatsRentingSystem.API.Data;
using BoatsRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BoatsController : ControllerBase
{
    private readonly RentalContext _context;

    public BoatsController(RentalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Boat>>> GetBoats()
    {
        return await _context.Boats.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Boat>> GetBoat(int id)
    {
        var boat = await _context.Boats.FindAsync(id);

        if (boat == null)
        {
            return NotFound();
        }

        return boat;
    }

    [HttpPost]
    public async Task<ActionResult<Boat>> PostBoat(Boat boat)
    {
        _context.Boats.Add(boat);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBoat", new { id = boat.BoatId }, boat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBoat(int id, Boat boat)
    {
        if (id != boat.BoatId)
        {
            return BadRequest();
        }

        _context.Entry(boat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BoatExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBoat(int id)
    {
        var boat = await _context.Boats.FindAsync(id);
        if (boat == null)
        {
            return NotFound();
        }

        _context.Boats.Remove(boat);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BoatExists(int id)
    {
        return _context.Boats.Any(e => e.BoatId == id);
    }
}