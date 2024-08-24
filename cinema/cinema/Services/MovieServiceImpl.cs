using System.Diagnostics;
using System.Globalization;
using System.Security.Policy;
using cinema.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cinema.Services
{
    public class MovieServiceImpl : MovieService
    {
        public MyDbContext db;
        public MovieServiceImpl(MyDbContext _db)
        {
            db = _db;
        }

        public bool create(Movie movie)
        {
            db.Movies.Add(movie);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll(bool status, string date, int cinemaId)
        {
            var date1 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(date1);
            return db.Movies
     .Where(m => m.Status == status && m.Showtimes.Any(m => m.ShowDate.Date == date1.Date))
     .Select(m => new
     {
         Id = m.Id,
         Title = m.Title,
         Description = m.Description,
         Duration = m.Duration,
         Genre = m.Genre,
         ReleaseDate = m.ReleaseDate,
         Age = m.Age,
         Trailer = m.Trailer,
         Director = m.Director,
         Actor = m.Actor,
         Publisher = m.Publisher,
         Status = m.Status,
         Photo = m.Photo,
         Showtimes = m.Showtimes.Where(m => m.ShowDate.Date == date1.Date && m.CinemaId == cinemaId).Select(s => new
         {
             Id = s.Id,
             CinemaName = s.Cinema.Name,
             ShowDate = s.ShowDate.ToString("dd/MM/yyyy HH:mm"),
             SubId = s.SubId,
             SubName = s.Sub.Name,
         }).ToList(),
         ListSubId = m.Showtimes
             .Select(s => s.SubId)
             .ToList()
     })
     .ToList();
        }

        public dynamic findAll(bool status)
        {
            return db.Movies
    .Where(m => m.Status == status)
    .Select(m => new
    {
        Id = m.Id,
        Title = m.Title,
        Description = m.Description,
        Duration = m.Duration,
        Genre = m.Genre,
        ReleaseDate = m.ReleaseDate,
        Age = m.Age,
        Trailer = m.Trailer,
        Director = m.Director,
        Actor = m.Actor,
        Publisher = m.Publisher,
        Status = m.Status,
        Photo = m.Photo,
        Showtimes = m.Showtimes.Select(s => new
        {
            Id = s.Id,
            CinemaName = s.Cinema.Name,
            ShowDate = s.ShowDate.ToString("dd/MM/yyyy HH:mm"),
            SubId = s.SubId,
            SubName = s.Sub.Name,
        }).ToList(),
        ListSubId = m.Showtimes
            .Select(s => s.SubId)
            .ToList()
    })
    .ToList();
        }

        public dynamic findById(int id)
        {
            return db.Movies.Where(m => m.Id == id).Select(m => new
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Duration = m.Duration,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate,
                Age = m.Age,
                Trailer = m.Trailer,
                Director = m.Director,
                Actor = m.Actor,
                Publisher = m.Publisher,
                Status = m.Status,
                Photo = m.Photo,
                Showtimes = m.Showtimes.Select(s => new
                {
                    Id = s.Id,
                    CinemaName = s.Cinema.Name,
                    ShowDate = s.ShowDate.ToString("dd/MM/yyyy HH:mm"),
                    SubId = s.SubId,
                    SubName = s.Sub.Name,
                }).ToList(),
                ListSubId = m.Showtimes
            .Select(s => s.SubId)
            .ToList()
            }).FirstOrDefault();
        }

        public dynamic findMovie(bool status, string date, int cinemaId, int movieId)
        {
            var date1 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(date1);
            return db.Movies
     .Where(m => m.Status == status && m.Showtimes.Any(m => m.ShowDate.Date == date1.Date && m.CinemaId == cinemaId) && m.Id == movieId)
     .Select(m => new
     {
         Id = m.Id,
         Title = m.Title,
         Description = m.Description,
         Duration = m.Duration,
         Genre = m.Genre,
         ReleaseDate = m.ReleaseDate,
         Age = m.Age,
         Trailer = m.Trailer,
         Director = m.Director,
         Actor = m.Actor,
         Publisher = m.Publisher,
         Status = m.Status,
         Photo = m.Photo,
         Showtimes = m.Showtimes.Where(m => m.ShowDate.Date == date1.Date && m.CinemaId == cinemaId).Select(s => new
         {
             Id = s.Id,
             CinemaName = s.Cinema.Name,
             ShowDate = s.ShowDate.ToString("dd/MM/yyyy HH:mm"),
             SubId = s.SubId,
             SubName = s.Sub.Name,
         }).ToList(),
         ListSubId = m.Showtimes
             .Select(s => s.SubId)
             .ToList()
     })
     .ToList();
        }

        public Movie findById1(int id)
        {
            return db.Movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public bool update(Movie movie)
        {
            db.Movies.Update(movie);
            return db.SaveChanges() > 0;
        }
    }
}
