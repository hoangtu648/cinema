using cinema.Models;
using System.Globalization;
using System.Security.Policy;

namespace cinema.Services
{
    public class ShowTimeServiceImpl : ShowTimeService
    {
        public MyDbContext db;
        public ShowTimeServiceImpl(MyDbContext _db)
        {
            db = _db;
        }

        public dynamic checkSeat(int id)
        {
            return db.Showtimes.Where(s => s.Id == id).Select(s => new
            {
                Bookings = s.Bookings.Select(b => new
                {

                    BookingDetails = b.BookingDetails.Select(b => new
                    {
                        SeatId = b.SeatId,
                        SeatName = b.Seat.Name
                    }).ToList()
                })
            }).FirstOrDefault();
        }

        public dynamic findById(int id)
        {
            return db.Showtimes.Where(s => s.Id == id).Select(s => new
            {
                Id = s.Id,
                Title = s.Movie.Title,
                MovieId = s.MovieId,
                Description = s.Movie.Description,
                Duration = s.Movie.Duration,
                Genre = s.Movie.Genre,
                ReleaseDate = s.Movie.ReleaseDate,
                Age = s.Movie.Age,
                Trailer = s.Movie.Trailer,
                Director = s.Movie.Director,
                Actor = s.Movie.Actor,
                Publisher = s.Movie.Publisher,
                Photo = s.Movie.Photo,
                CinemaId = s.CinemaId,
                CinemaName = s.Cinema.Name,
                RoomId = s.RoomId,
                ShowDate = s.ShowDate,
                ShowTime = s.ShowDate.ToString("HH:mm"),
                SubId = s.SubId,
                SubName = s.Sub.Name,
                Status = s.Status
            }).FirstOrDefault();
        }

        public Showtime findById1(int id)
        {
            return db.Showtimes.Where(s => s.Id == id).FirstOrDefault();
        }

        public dynamic findAll()
        {
            return db.Showtimes.Where(s => s.Status == true).Select(s => new
            {
                Id = s.Id,
                Title = s.Movie.Title,
                Description = s.Movie.Description,
                Duration = s.Movie.Duration,
                Genre = s.Movie.Genre,
                ReleaseDate = s.Movie.ReleaseDate,
                Age = s.Movie.Age,
                Trailer = s.Movie.Trailer,
                Director = s.Movie.Director,
                Actor = s.Movie.Actor,
                Publisher = s.Movie.Publisher,
                Photo = s.Movie.Photo,
                CinemaName = s.Cinema.Name,
                ShowDate = s.ShowDate,
                ShowTime = s.ShowDate.ToString("HH:mm"),
                SubId = s.SubId,
                roomId = s.RoomId,
                RoomName = s.Room.Name,
                SubName = s.Sub.Name,
                Status = s.Status
            }).ToList();
        }

        public dynamic findMovie(bool status, string date, int cinemaId, int movieId)
        {
            var date1 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(date1);
            return db.Showtimes.Where(s => s.Status == status && s.ShowDate.Date == date1.Date && s.CinemaId == cinemaId && s.MovieId == movieId).Select(s => new
            {
                Id = s.Id,
                Title = s.Movie.Title,
                Description = s.Movie.Description,
                Duration = s.Movie.Duration,
                Genre = s.Movie.Genre,
                ReleaseDate = s.Movie.ReleaseDate,
                Age = s.Movie.Age,
                Trailer = s.Movie.Trailer,
                Director = s.Movie.Director,
                Actor = s.Movie.Actor,
                Publisher = s.Movie.Publisher,
                Photo = s.Movie.Photo,
                CinemaId = s.Cinema.Id,
                CinemaName = s.Cinema.Name,
                ShowDate = s.ShowDate,
                ShowTime = s.ShowDate.ToString("HH:mm"),
                SubId = s.SubId,
                RoomId = s.RoomId,
                RoomName = s.Room.Name,
                SubName = s.Sub.Name
            }).ToList();
        }

        public dynamic findAllByCinemaId(int cinemaId)
        {
            return db.Showtimes
                .Where(s => s.Status == true && s.CinemaId == cinemaId)
                .Select(s => new
                {
                    Id = s.Id,
                    Title = s.Movie.Title,
                    Description = s.Movie.Description,
                    Duration = s.Movie.Duration,
                    Genre = s.Movie.Genre,
                    ReleaseDate = s.Movie.ReleaseDate,
                    Age = s.Movie.Age,
                    Trailer = s.Movie.Trailer,
                    Director = s.Movie.Director,
                    Actor = s.Movie.Actor,
                    Publisher = s.Movie.Publisher,
                    Photo = s.Movie.Photo,
                    CinemaName = s.Cinema.Name,
                    ShowDate = s.ShowDate,
                    ShowTime = s.ShowDate.ToString("HH:mm"),
                    SubId = s.SubId,
                    RoomId = s.RoomId,
                    RoomName = s.Room.Name,
                    SubName = s.Sub.Name,
                    Status = s.Status
                })
                .ToList();
        }
        public bool create(Showtime showtime)
        {
            showtime.Status = true;

            db.Showtimes.Add(showtime);
            return db.SaveChanges() > 0;
        }

        public bool update(Showtime showtime)
        {

            db.Showtimes.Update(showtime);
            return db.SaveChanges() > 0;
        }

        public dynamic listMovieTomorrow()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            return db.Showtimes
                .Where(s => s.Status == true && s.ShowDate.Date == tomorrow)
                .Select(s => new
                {
                    Id = s.Id,
                    Title = s.Movie.Title,
                    Description = s.Movie.Description,
                    Duration = s.Movie.Duration,
                    Genre = s.Movie.Genre,
                    ReleaseDate = s.Movie.ReleaseDate,
                    Age = s.Movie.Age,
                    Trailer = s.Movie.Trailer,
                    Director = s.Movie.Director,
                    Actor = s.Movie.Actor,
                    Publisher = s.Movie.Publisher,
                    Photo = s.Movie.Photo,
                    CinemaName = s.Cinema.Name,
                    ShowDate = s.ShowDate,
                    ShowTime = s.ShowDate.ToString("HH:mm"),
                    SubId = s.SubId,
                    roomId = s.RoomId,
                    RoomName = s.Room.Name,
                    SubName = s.Sub.Name,
                    Status = s.Status
                })
                .ToList();
        }
    }
}
