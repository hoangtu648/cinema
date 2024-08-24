using cinema.Models;

namespace cinema.Services
{
    public interface BookingService
    {
        public bool create(Booking booking);
        public bool createBookingDetails(BookingDetail bookingDetail);
        public dynamic findSeatByName(string name);

        public dynamic findAll();

        public dynamic findById(int id);

      
    }
}
