using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BoatsRentingSystem.Models
{
    public class Boat
    {
        [Required]
        public required long BoatId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(50)]
        public required string Type { get; set; }

        [AllowNull]
        public int Capacity { get; set; }

        [Required]
        public required decimal PricePerHour { get; set; }

        [Required]
        public required bool Availability { get; set; }
    }

}
