using cinema.Models;

namespace cinema.Services
{
    public class RatingServiceImpl : RatingService
    {
        public MyDbContext db;

        public RatingServiceImpl(MyDbContext _db)
        {
            db = _db;
        }

        public double avgByMovieId(int movieId)
        {
            var ratings = db.Ratings.Where(r => r.MovieId == movieId).ToList();

            // Tính điểm trung bình
            double averageRating = ratings.Any() ? ratings.Average(r => r.Rate) : 0.0;

            return averageRating;
        }

        public dynamic create(Rating rating)
        {
            db.Ratings.Add(rating);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll(int movieId)
        {
            return db.Ratings.Where(r => r.MovieId == movieId).OrderByDescending(r => r.Id).Select(r => new
            {
                Id = r.Id,
                Comment = r.Comment,
                Rate = r.Rate,
                AccountId = r.Account.Username,

            }).ToList();
        }

        public dynamic findAll()
        {
            return db.Ratings.Select(r => new
            {
                Id = r.Id,
                Comment = r.Comment,
                Rate = r.Rate,
                AccountId = r.Account.Username,


            }).ToList();
        }

        public dynamic findAll(int movieId, bool status)
        {
            return db.Ratings.Where(r => r.MovieId == movieId && r.Status == status).OrderByDescending(r => r.Id).Select(r => new
            {
                Id = r.Id,
                Comment = r.Comment,
                Rate = r.Rate,
                AccountId = r.Account.Username,

            }).ToList();
        }
    }
}
