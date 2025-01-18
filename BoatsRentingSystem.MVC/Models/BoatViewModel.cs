namespace BoatsRentingSystem.MVC.Models
{
    public class BoatViewModel
    {
        public long BoatId { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public bool Availability { get; set; }
    }

}
