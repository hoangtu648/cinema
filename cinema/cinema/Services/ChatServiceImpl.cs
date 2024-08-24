using cinema.Models;
using Microsoft.AspNetCore.Identity;

namespace cinema.Services
{
    public class ChatServiceImpl : ChatService
    {
        public MyDbContext db;
        public ChatServiceImpl(MyDbContext _db)
        {
            db = _db;
          
        }
        public dynamic findChatByAccountId(int accountId)
        {
            return db.Chats.Where(c => c.AccountId == accountId).Select(c => new
            {
                Id = c.Id,
                AccountId = c.AccountId,
                Name = c.Account.Username,
                Message = c.Message,
                Role = c.Role,
                Time = c.Time.ToString("dd/MM/yyyy HH:mm:ss"),
            }).ToList();
        }
        public dynamic listUser()
        {
            return db.Chats
                .GroupBy(c => c.AccountId)
                .Select(g => new
                {
                    AccountId = g.Key,
                    Name = db.Accounts.Where(a => a.Id == g.Key).FirstOrDefault().Username,
                    Messages = g.Select(c => new
                    {
                        Id = c.Id,
                        Message = c.Message,
                        Role = c.Role,
                        Time = c.Time.ToString("dd/MM/yyyy HH:mm:ss"),
                    }).ToList()
                })
                .ToList();
        }


        public bool newChat(Chat chat)
        {
            db.Chats.Add(chat);
            return db.SaveChanges() > 0;
        }
    }
}
