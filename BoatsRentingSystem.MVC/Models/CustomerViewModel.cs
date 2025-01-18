namespace BoatsRentingSystem.MVC.Models
{
    public class CustomerViewModel
    {
        public long CustomerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

}
