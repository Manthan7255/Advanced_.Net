using Microsoft.AspNetCore.Mvc;
using HotelManagement.Web.Data;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new HomeStats
            {
                TotalRooms = await _context.Rooms.CountAsync(),
                AvailableRooms = await _context.Rooms.CountAsync(r => r.IsAvailable),
                TotalGuests = await _context.Guests.CountAsync(),
                ActiveBookings = await _context.Bookings.CountAsync(b => b.Status == "Confirmed" || b.Status == "CheckedIn")
            };

            return View(stats);
        }
    }
}