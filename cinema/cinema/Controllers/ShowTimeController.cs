using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/showTime")]
    public class ShowTimeController : Controller
    {
        public ShowTimeService showTimeService;
        public ShowTimeController(ShowTimeService _showTimeService)
        {
            this.showTimeService = _showTimeService;
        }

        [HttpGet("findById/{id}")]
        [Produces("application/json")]
        public IActionResult findById(int id)
        {
            try
            {
                return Ok(showTimeService.findById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findAll")]
        [Produces("application/json")]
        public IActionResult findALl()
        {
            try
            {
                return Ok(showTimeService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [Produces("application/json")]

        public IActionResult delete(int id)
        {
            var showTime = showTimeService.findById1(id);
            showTime.Status = false;

            try
            {
                return Ok(new
                {
                    Status = showTimeService.update(showTime)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("checkSeat/{bookingId}")]
        [Produces("application/json")]
        public IActionResult checkSeat(int bookingId)
        {

            try
            {

                return Ok(showTimeService.checkSeat(bookingId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findAllByCinema/{cinemaId}")]
        [Produces("application/json")]
        public IActionResult findAllByCinemaId(int cinemaId)
        {

            try
            {
                return Ok(showTimeService.findAllByCinemaId(cinemaId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Showtime showtime)
            
        {
            showtime.Status = true;

            try
            {
                return Ok(new
                {
                    Status = showTimeService.create(showtime)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findMovie")]
        [Produces("application/json")]
        public IActionResult findMovie(string date, int cinemaId, int movieId)
        {
            try
            {
                return Ok(showTimeService.findMovie(true, date, cinemaId, movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult edit([FromBody] Showtime showtime)

        {
            showtime.Status = true;
            try
            {
                return Ok(new
                {
                    Status = showTimeService.update(showtime)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
