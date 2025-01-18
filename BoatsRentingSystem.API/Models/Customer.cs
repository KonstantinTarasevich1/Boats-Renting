using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BoatsRentingSystem.Models
{
    public class Customer
    {
        [Required]
        public required long CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        [AllowNull]
        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        [AllowNull]
        public DateOnly? Birthday { get; set; }

        [Required]
        public required DateTime RegistrationDate { get; set; }
    }
}
