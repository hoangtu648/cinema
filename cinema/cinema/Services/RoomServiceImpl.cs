using cinema.Models;

namespace cinema.Services
{
    public class RoomServiceImpl : RoomService
    {
        public MyDbContext db;

        public RoomServiceImpl(MyDbContext _db)
        {
            db = _db;

        }

        public bool create(Room room)
        {
            db.Rooms.Add(room);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Rooms.Where(r => r.Status == true).Select(r => new
            {
                Id = r.Id,
                Name = r.Name,
                Cinema = r.Cinema.Name,
                Quantity = r.Quantity,
                Status = r.Status,
                CinemaId = r.CinemaId,
            }).ToList();
        }

        public dynamic findById1(int id)
        {
            return db.Rooms.Where(r => r.Id == id).Select(r => new
            {
                Id = r.Id,
                Name = r.Name,
                Cinema = r.Cinema.Name,
                Quantity = r.Quantity,
                Status = r.Status,
                CinemaId = r.CinemaId,
            }).FirstOrDefault();
        }

        public Room findById(int id)
        {
            return db.Rooms.Where(r => r.Id == id).FirstOrDefault();
        }

        public bool update(Room room)
        {
           
            db.Rooms.Update(room);
            return db.SaveChanges() > 0;
        }

        public dynamic findAllByCinema(int cinemaId)
        {
            return db.Rooms
                .Where(r => r.Status == true && r.CinemaId == cinemaId)
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Name,
                    Cinema = r.Cinema.Name,
                    Quantity = r.Quantity,
                    Status = r.Status,
                    CinemaId = r.CinemaId,
                }).ToList();
        }
    }
}
