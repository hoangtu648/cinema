using cinema.Models;

namespace cinema.Services
{
    public class ComboServiceImpl : ComboService
    {
        public MyDbContext db;
        public ComboServiceImpl(MyDbContext _db)
        {
            db = _db;
        }

        public bool createComboDetails(ComboDetail comboDetail)
        {
            db.ComboDetails.Add(comboDetail);
            return db.SaveChanges() > 0;
        }

        public dynamic findAll()
        {
            return db.Combos.Where(c => c.Status == true ).Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Status = c.Status
            }).ToList();
        }
        public bool create(Combo combo)
        {
            db.Combos.Add(combo);
            return db.SaveChanges() > 0;
        }


        public dynamic findById1(int id)
        {
            return db.Combos.Where(c => c.Id == id).Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Status = c.Status
            }).FirstOrDefault();
        }

        public Combo findById(int id)
        {
            return db.Combos.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool update(Combo combo)
        {

            db.Combos.Update(combo);
            return db.SaveChanges() > 0;
        }

    }
}

