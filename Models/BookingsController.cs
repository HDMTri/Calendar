using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Models
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // API for FullCalendar

        // GET: /Bookings/GetBookings
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var events = await _context.Bookings.Select(b => new
            {
                id = b.BookingID,
                title = b.Title,
                start = b.StartTime,
                end = b.EndTime,
                description = b.Description
            }).ToListAsync();

            return new JsonResult(events);
        }

        // POST: /Bookings/CreateBooking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);  // Add booking to the database
                await _context.SaveChangesAsync();  // Save changes to the database
                return Ok();  // Return success response
            }
            return BadRequest(ModelState);  // Return error response if validation fails
        }      
    }
}