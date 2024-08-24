using cinema.Models;

namespace cinema.Services
{
    public class SubServiceImpl : SubService
    {
        public MyDbContext db;

        public SubServiceImpl(MyDbContext _db)
        {
            db = _db;

        }

        public bool create(Sub sub)
        {
            db.Subs.Add(sub);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Subs.Where(s => s.Status == true).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Created = s.Created,
                Status= s.Status,
                
            }).ToList();
        }

        public dynamic findById1(int id)
        {
            return db.Subs.Where(s => s.Id == id).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Created = s.Created,
                Status = s.Status,
            }).FirstOrDefault();
        }

        public Sub findById(int id)
        {
            return db.Subs.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool update(Sub sub)
        {

            db.Subs.Update(sub);
            return db.SaveChanges() > 0;
        }
    }
}
