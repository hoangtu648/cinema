using cinema.Models;

namespace cinema.Services
{
    public interface PaymentService 
    {
        public bool create(Payment payment);
        public dynamic findById(int id);
        public dynamic findAll();
    }
}
