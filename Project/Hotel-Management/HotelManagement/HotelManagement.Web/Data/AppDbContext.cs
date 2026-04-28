using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace HotelManagement.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed sample data
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, RoomNumber = "101", Type = "Single", PricePerNight = 1500, IsAvailable = true, Description = "Cozy single room" },
                new Room { RoomId = 2, RoomNumber = "102", Type = "Double", PricePerNight = 2500, IsAvailable = true, Description = "Spacious double room" },
                new Room { RoomId = 3, RoomNumber = "201", Type = "Suite", PricePerNight = 5000, IsAvailable = true, Description = "Luxury suite with balcony" },
                new Room { RoomId = 4, RoomNumber = "202", Type = "Deluxe", PricePerNight = 3500, IsAvailable = false, Description = "Premium deluxe room" }
            );
        }
    }
}