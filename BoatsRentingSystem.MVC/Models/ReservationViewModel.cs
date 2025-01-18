namespace BoatsRentingSystem.MVC.Models
{
    public class ReservationViewModel
    {
        public long ReservationId { get; set; }
        public long BoatId { get; set; }

        public BoatViewModel Boat { get; set; }

        public CustomerViewModel Customer { get; set; }
        public long CustomerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Description { get; set; }
    }

}
