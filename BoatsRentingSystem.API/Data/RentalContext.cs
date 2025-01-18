using BoatsRentingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BoatsRentingSystem.API.Data
{
    public class RentalContext : DbContext
    {
        public RentalContext(DbContextOptions<RentalContext> options) : base(options)
        {
        }

        public DbSet<Boat> Boats { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
