using cinema.Models;

namespace cinema.Services
{
    public interface ComboService
    {
        public dynamic findAll();

        public bool createComboDetails(ComboDetail comboDetail);

        public Boolean create(Combo combo);

        public Boolean update(Combo combo);

        public Combo findById(int id);

        public dynamic findById1(int id);
    }
}
