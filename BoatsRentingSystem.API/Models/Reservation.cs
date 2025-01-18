using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BoatsRentingSystem.Models
{
    public class Reservation
    {
        [Required]
        public required long ReservationId { get; set; }\

        [Required]
        public required long BoatId { get; set; }
        public Boat Boat { get; set; }

        [Required]
        public required long CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public required DateTime StartTime { get; set; }

        [Required]
        public required DateTime EndTime { get; set; }

        [Required]
        public required decimal TotalPrice { get; set; }

        [AllowNull]
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
