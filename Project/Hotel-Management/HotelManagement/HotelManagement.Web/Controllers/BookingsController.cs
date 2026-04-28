using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Web.Data;
using HotelManagement.Models;

namespace HotelManagement.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room);
            return View(await bookings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "Name");
            ViewData["RoomId"] = new SelectList(_context.Rooms.Where(r => r.IsAvailable), "RoomId", "RoomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,RoomId,CheckInDate,CheckOutDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.FindAsync(booking.RoomId);
                if (room != null)
                {
                    var days = (booking.CheckOutDate - booking.CheckInDate).Days;
                    booking.TotalAmount = room.PricePerNight * (days > 0 ? days : 1);
                    room.IsAvailable = false;
                }
                
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "Name", booking.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "Name", booking.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,GuestId,RoomId,CheckInDate,CheckOutDate,Status")] Booking booking)
        {
            if (id != booking.BookingId) return NotFound();

            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.FindAsync(booking.RoomId);
                if (room != null)
                {
                    var days = (booking.CheckOutDate - booking.CheckInDate).Days;
                    booking.TotalAmount = room.PricePerNight * (days > 0 ? days : 1);
                }
                
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "Name", booking.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var booking = await _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                var room = await _context.Rooms.FindAsync(booking.RoomId);
                if (room != null) room.IsAvailable = true;
                
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "CheckedOut";
                var room = await _context.Rooms.FindAsync(booking.RoomId);
                if (room != null) room.IsAvailable = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}