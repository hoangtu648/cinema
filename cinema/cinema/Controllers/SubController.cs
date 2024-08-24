using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/sub")]
    public class SubController : Controller
    {
        public SubService subService;
        public SubController(SubService _subService)
        {
            subService = _subService;
        }
        [HttpGet("findAll")]
        [Produces("application/json")]
     
        public IActionResult findAll()
        {
          
            try
            {
                return Ok(subService.findAll());
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Sub sub)
        {
            sub.Status = true;
            try
            {
                return Ok(new
                {
                    Status = subService.create(sub)
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
            var sub = subService.findById(id);
            sub.Status = false;
            try
            {
                return Ok(new
                {
                    Status = subService.update(sub)
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
        public IActionResult edit([FromBody] Sub sub)

        {
            sub.Status = true;
            try
            {
                return Ok(new
                {
                    Status = subService.update(sub)
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
                return Ok(subService.findById1(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
