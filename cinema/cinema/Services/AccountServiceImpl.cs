using cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
namespace cinema.Services
{

    public class AccountServiceImpl : AccountService
    {
        public MyDbContext db;
    
        public AccountServiceImpl(MyDbContext _db)
        {
            db = _db;
        
        }
        public bool checkLogin(string email, string password)
        {

            try
            {
                var account = db.Accounts.SingleOrDefault(a => a.Email == email);
                if (account != null)
                {
                    return BCrypt.Net.BCrypt.Verify(password, account.Password);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public dynamic findByUsername(string username)
        {
            return db.Accounts.Where(acc => acc.Username == username).Select(acc => new
            {
                Id = acc.Id,
                Username = acc.Username,
                Email = acc.Email,
                Password = acc.Password,
                Phone = acc.Phone,
                Gender = acc.Gender,
                Birthday = acc.Birthday,
                Created = acc.Created,
                Verify = acc.Verify,
                Securitycode = acc.Securitycode,
                Role = acc.Role
            }).FirstOrDefault();
        }

        public dynamic findByEmail(string email)
        {
            return db.Accounts.Where(acc => acc.Email == email).Select(acc => new
            {
                Id = acc.Id,
                Username = acc.Username,
                Email = acc.Email,
                Password = acc.Password,
                Phone = acc.Phone,
                Gender = acc.Gender,
                Birthday = acc.Birthday,
                Created = acc.Created,
                Verify = acc.Verify,
                Securitycode = acc.Securitycode,
                Role = acc.Role
            }).FirstOrDefault();
        }

        public bool register(Account account)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            db.Accounts.Add(account);
           return db.SaveChanges() > 0;
        }

        public bool update(Account account)
        { 
            db.Update(account);
            return db.SaveChanges() > 0;
        }

        public bool VerifyAccountAsync(string email)
        {
            // Chuẩn hóa email
            email = email.Trim().ToLower();
            Console.WriteLine($"Searching for email: {email}");

            // Tìm tài khoản theo email đã chuẩn hóa
            Account account =  db.Accounts
                .Where(a => a.Email.Trim().ToLower() == email)
                .FirstOrDefault();

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return false; // Tài khoản không tồn tại
            }

            // In chi tiết của đối tượng account
            Console.WriteLine($"Account found: Username={account.Username}, Email={account.Email}, Verify={account.Verify}");

            // Cập nhật trạng thái xác thực
            account.Verify = 1; // Giả định 1 là giá trị cho đã xác thực; điều chỉnh theo yêu cầu của bạn
            db.Accounts.Update(account);
            db.SaveChangesAsync();

            return true; // Xác thực thành công
        }

        public dynamic findAll()
        {
            return db.Accounts.Select(a => new
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.Password,
                Role = a.Role,
                Created = a.Created,
                Verify = a.Verify,
                Email = a.Email,
                Phone = a.Phone,
                Gender =a.Gender,
                Birthday = a.Birthday,
                Sercuritycode = a.Securitycode 
            }).ToList();
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await db.Accounts.SingleOrDefaultAsync(a => a.Email == email);
        }

        public async Task<bool> CreateAsync(Account account)
        {
            db.Accounts.Add(account);
            return await db.SaveChangesAsync() > 0;
        }

        public dynamic findByAccountId(int id)
        {
            return db.Accounts.Where(acc => acc.Id == id).Select(acc => new
            {
                Id = acc.Id,
                Username = acc.Username,
                Email = acc.Email,
                Password = acc.Password,
                Phone = acc.Phone,
                Gender = acc.Gender,
                Birthday = acc.Birthday,
                Created = acc.Created,
                Verify = acc.Verify,
                Securitycode = acc.Securitycode,
                Role = acc.Role
            }).FirstOrDefault();
        }
    }

  
}
