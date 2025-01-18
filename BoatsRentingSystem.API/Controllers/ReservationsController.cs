using BoatsRentingSystem.API.Data;
using BoatsRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoatsRentingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly RentalContext _context;

        public ReservationsController(RentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations([FromQuery] long boatId, [FromQuery] long customerId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var reservationsQuery = _context.Reservations.AsQueryable();

            if (boatId > 0)
            {
                reservationsQuery = reservationsQuery.Where(r => r.BoatId == boatId);
            }

            if (customerId > 0)
            {
                reservationsQuery = reservationsQuery.Where(r => r.CustomerId == customerId);
            }

            var reservations = await reservationsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.ReservationId }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(long id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
