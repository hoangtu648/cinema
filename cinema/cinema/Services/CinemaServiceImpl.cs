using cinema.Models;

namespace cinema.Services
{
    public class CinemaServiceImpl : CinemaService
    {
        public MyDbContext db;
        public CinemaServiceImpl(MyDbContext _db)
        {
            db = _db;
        }
        public dynamic findAll()
        {
            return db.Cinemas.Select(c => new { 
                Id = c.Id,
                Name = c.Name,
                City = c.City,
                District = c.District,
                Status = c.Status,
            }).ToList();
        }
    }
}
