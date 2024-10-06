namespace Calendar.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsFullDay { get; set; } // Optional: full-day event support
    }
}
