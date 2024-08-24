using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/room")]
    public class RoomController : Controller
    {
        public RoomService roomService;
        public RoomController(RoomService _roomService)
        {
            roomService = _roomService;
        }
        [HttpGet("findAll")]
        [Produces("application/json")]
     
        public IActionResult findAll()
        {
          
            try
            {
                return Ok(roomService.findAll());
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Room room)
        {
            room.Status = true;
            try
            {
                return Ok(new
                {
                    Status = roomService.create(room)
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
            var room = roomService.findById(id);
            room.Status = false;
            try
            {
                return Ok(new
                {
                    Status = roomService.update(room)
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
        public IActionResult edit([FromBody] Room room)

        {
            room.Status = true;
            try
            {
                return Ok(new
                {
                    Status = roomService.update(room)
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
                return Ok(roomService.findById1(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findByCinemaId/{id}")]
        [Produces("application/json")]

        public IActionResult findByCinemaId(int id)
        {

            try
            {
                return Ok(roomService.findAllByCinema(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
