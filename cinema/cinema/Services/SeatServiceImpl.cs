using cinema.Models;

namespace cinema.Services
{
    public class SeatServiceImpl : SeatService
    {
        public MyDbContext db;

        public SeatServiceImpl(MyDbContext _db)
        {
            db = _db;

        }

        public bool create(Seat seat)
        {
            db.Seats.Add(seat);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Seats.Where(s => s.Status == true).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Room = s.Room.Name,
                RoomId=s.RoomId,
                SeatType=s.SeatType,
                Price=s.Price,
                Status= s.Status,
                
            }).ToList();
        }

        public dynamic findById1(int id)
        {
            return db.Seats.Where(s => s.Id == id).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                RoomID = s.RoomId,
                SeatType = s.SeatType,
                Price = s.Price,
                Status = s.Status,
            }).FirstOrDefault();
        }

        public Seat findById(int id)
        {
            return db.Seats.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool update(Seat seat)
        {

            db.Seats.Update(seat);
            return db.SaveChanges() > 0;
        }
    
    }
}
