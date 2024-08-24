using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/seat")]
    public class SeatController : Controller
    {
        public SeatService seatService;
        public SeatController(SeatService _seatService)
        {
            seatService = _seatService;
        }
        [HttpGet("findAll")]
        [Produces("application/json")]

        public IActionResult findAll()
        {

            try
            {
                return Ok(seatService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Seat seat)
        {
            seat.Status = true;
            try
            {
                return Ok(new
                {
                    Status = seatService.create(seat)
                });
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
            var seat = seatService.findById(id);
            seat.Status = false;
            try
            {
                return Ok(new
                {
                    Status = seatService.update(seat)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("edit")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult edit([FromBody] Seat seat)

        {
            seat.Status = true;
            try
            {
                return Ok(new
                {
                    Status = seatService.update(seat)
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
                return Ok(seatService.findById1(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}