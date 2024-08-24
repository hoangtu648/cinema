using cinema.Models;

namespace cinema.Services
{
    public class FollowServiceImpl : FollowService
    {
        public MyDbContext db;

        public FollowServiceImpl(MyDbContext _db)
        {
            db = _db;

        }
        public bool create(Follow follow)
        {
            db.Follows.Update(follow);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Follows.Select(f => new
            {
                Id = f.Id,
                AccountId = f.AccountId,
                Status = f.Status,
                Email = f.Account.Email
            }).ToList();
        }

        public dynamic findById(int id)
        {
            return db.Follows.Where(f => f.AccountId == id).Select(f => new
            {
                Id = f.Id,
                AccountId = f.AccountId,
                Status = f.Status
            }).FirstOrDefault();
        }
    }
}
