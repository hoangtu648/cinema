using cinema.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace cinema.Services
{
    public class BookingServiceImpl : BookingService
    {
        public MyDbContext db;
        public BookingServiceImpl(MyDbContext _db)
        {
            db = _db;
        }

   

        public bool create(Booking booking)
        {
            db.Bookings.Add(booking);
            return db.SaveChanges() > 0;
        }

        public bool createBookingDetails(BookingDetail bookingDetail)
        {
            db.BookingDetails.Add(bookingDetail);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Bookings.Select(b => new
            {
                Id = b.Id,
                Created = b.Created,
                Name = b.Name,
                Movie = b.Showtime.Movie.Title,
                Email = b.Email,
                Phone = b.Phone,
                CountTicket = b.BookingDetails.Count(),
                CountCombo = b.ComboDetails.Count(),

            }).ToList();
        }

        public dynamic findById(int id)
        {
            return db.Bookings.Where(b => b.Id == id).Select(b => new
            {
                ShowTime = b.Showtime.ShowDate,
                Cinema = b.Showtime.Cinema.Name,
                Movie = b.Showtime.Movie.Title,
                Sub = b.Showtime.Sub.Name,
                Room = b.Showtime.Room.Name,
                BookingDetails = b.BookingDetails.Select(b => new
                {
                    Seat = b.Seat.Name,

                }).ToList(),
                ComboDetails = b.ComboDetails.Select(c => new
                {
                    Combo = c.Combo.Name,
                    Quantity = c.Quantity
                }).ToList()
            }).FirstOrDefault();
        }

        public dynamic findSeatByName(string name)
        {
            return db.Seats.Where(s => s.Name == name).FirstOrDefault();
        }
    }
}
