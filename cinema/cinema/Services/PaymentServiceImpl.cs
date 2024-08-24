using cinema.Models;

namespace cinema.Services
{
    public class PaymentServiceImpl : PaymentService
    {
        public MyDbContext db;
        public PaymentServiceImpl(MyDbContext _db)
        {
            db = _db;
        }

        public bool create(Payment payment)
        {
            db.Payments.Add(payment);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Payments.Where(p => p.Status == true).Select(p => new
            {
                Id = p.Id,
                BookingId = p.BookingId,
                PaymentType = p.PaymentType,
                TransactionNo = p.TransactionNo,
                TicketNumber = p.TicketNumber,
                Qr = p.Qr,
                Created = p.Created,
                Description = p.Description,
                Price = p.Price,
                Status = p.Status,
            }).ToList();
        }

        public dynamic findById(int id)
        {
            return db.Payments.Where(p => p.Id == id).Select(p => new
            {
                Id = p.Id,
                Cinema = p.Booking.Showtime.Cinema.Name,
                ShowDate = p.Booking.Showtime.ShowDate.ToString("dd/MM/yyyy"),
                ShowTime = p.Booking.Showtime.ShowDate.ToString("HH:mm"),
                SubName = p.Booking.Showtime.Sub.Name,
                Title = p.Booking.Showtime.Movie.Title,
                Photo = p.Booking.Showtime.Movie.Photo,
                Duration = p.Booking.Showtime.Movie.Duration,
                TicketNumber = p.TicketNumber,
                Price = p.Price,
                TransactionNo = p.TransactionNo,
                Room = p.Booking.Showtime.Room.Name,
                BookingDetails = p.Booking.BookingDetails.Select(b => new
                {
                    seatId = b.Seat.Name
                })
            }).FirstOrDefault();
        }
    }
}
