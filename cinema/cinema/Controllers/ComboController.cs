using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/combo")]
    public class ComboController : Controller
    {
        public ComboService comboService;
        public ComboController(ComboService _comboService)
        {
            this.comboService = _comboService;
        }

        [HttpGet("findAll")]
        [Produces("application/json")]
        public IActionResult findAll()
        {
            try
            {
                return Ok(comboService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Combo combo)
        {
            combo.Status = true;
            try
            {
                return Ok(new
                {
                    Status = comboService.create(combo)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("createComboDetails")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult createComboDetails([FromBody] ComboDetail comboDetail)
        {
            comboDetail.Status = true;
            try
            {
                return Ok(new
                {
                    Status = comboService.createComboDetails(comboDetail)
                }

                );
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
            var combo = comboService.findById(id);
            combo.Status = false;
            try
            {
                return Ok(new
                {
                    Status = comboService.update(combo)
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
                return Ok(comboService.findById1(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}