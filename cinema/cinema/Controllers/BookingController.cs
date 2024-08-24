using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        public BookingService bookingService;
        public BookingController(BookingService _bookingService)
        {
            bookingService = _bookingService;
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Booking booking)
        {   
           booking.Created = DateTime.Now;
            try
            {
                return Ok(new
                {
                    Status = bookingService.create(booking),
                    Id = booking.Id,
                });
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("createBookingDetails")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult createBookingDetails([FromBody] BookingDetail booking)
        {
            
            booking.Status = true;
            try
            {
                return Ok(new
                {
                    Status = bookingService.createBookingDetails(booking),
                 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("findSeatByName/{name}")]
        [Produces("application/json")]
        public IActionResult findSeatByName(string name)
        {
           
            try
            {
                return Ok(new
                {
                   Id = bookingService.findSeatByName(name).Id,
                   Price = bookingService.findSeatByName(name).Price
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("findAll")]
        [Produces("application/json")]
        public IActionResult findAll()
        {

            try
            {
                return Ok(bookingService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("findById/{bookingId}")]
        [Produces("application/json")]
        public IActionResult findById(int bookingId)
        {

            try
            {
                return Ok(bookingService.findById(bookingId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
