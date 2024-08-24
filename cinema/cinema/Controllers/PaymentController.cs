using cinema.Helpers;
using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        public PaymentService paymentService;
        public MailHelper mailHelper;
        public SMSHelper smsHelper;
        public PaymentController(PaymentService _paymentService, MailHelper _mailHelper, SMSHelper _smsHelper)
        {
            paymentService = _paymentService;
            mailHelper = _mailHelper;
            smsHelper = _smsHelper;
        }
        [HttpGet("findAll")]
        [Produces("application/json")]

        public IActionResult findAll()
        {

            try
            {
                return Ok(paymentService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Payment payment)
        {
            payment.Created = DateTime.Now;
            payment.Status = true;
            try
            {
                return Ok(new
                {
                    Status = paymentService.create(payment),
                    Id = payment.Id
                });
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
                return Ok(paymentService.findById(id));
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
        [HttpGet("sendSMS")]
        [Produces("application/json")]
      
        public IActionResult sendSMS(string body)
        {

            try
            {
                return Ok(new
                {
                    Status = smsHelper.sendSMS(body)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    }
