using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Diagnostics;
using cinema.Helpers;

namespace cinema.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        public AccountService accountService;
        public MailHelper mailHelper;
        public IWebHostEnvironment webHostEnvironment;
        public AccountController(AccountService _accountService, MailHelper _mailHelper, IWebHostEnvironment _webHostEnvironment)
        {
            accountService = _accountService;
            mailHelper = _mailHelper;
            webHostEnvironment = _webHostEnvironment;
        }
        [HttpPost("login")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Login([FromBody] Account account)
        {
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("123"));
            try
            {
                return Ok(new
                {
                    Status = accountService.checkLogin(account.Email, account.Password),
                    Role = accountService.findByEmail(account.Email).Role
                });
            }
            catch (Exception ex){
                return Ok(new
                {
                    Status = false
                });
              
            }
        }
        [HttpGet("findAll")]
        [Produces("application/json")]
        public IActionResult findAll()
        {
            try
            {
                return Ok(accountService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("findById/{id}")]
        [Produces("application/json")]
        public IActionResult findById(int id)
        {
            try
            {
                return Ok(accountService.findByAccountId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Register([FromBody] Account account)
        {
            account.Role = 1;
            account.Verify = 0;
            account.Created = DateTime.Now;
            try
            {
                return Ok(new
                {
                    Status = accountService.register(account)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sendMail")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult sendMail([FromBody] Email email)
        {

            try
            {
                return Ok(new
                {
                    Status = mailHelper.Send(email.From, email.To, email.Subject, email.Content)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult update([FromBody] Account account)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            try
            {
                return Ok(new
                {
                    Status = accountService.update(account)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify")]
        public IActionResult Verify([FromQuery] string email)
        {
            try
            {
                // Gọi phương thức VerifyAccountAsync từ accountService
                var status =  accountService.VerifyAccountAsync(email);

                // Trả về kết quả với status
                return Ok(new
                {
                    Status = status
                });
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("findByEmail/{email}")]
        [Produces("application/json")]

        public IActionResult findByEmail(String email)
        {
            try
            {
                return Ok(accountService.findByEmail(email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

    }

}
