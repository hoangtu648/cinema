using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/rating")]
    public class RatingController : Controller
    {
        public RatingService ratingService;

        public RatingController(RatingService _ratingService)
        {
            ratingService = _ratingService;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult create([FromBody] Rating rating)
        {
            rating.Created = DateTime.Now;
            rating.Status = true;
            try
            {
                return Ok(new
                {
                    Status = ratingService.create(rating)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("findAll/{movieId}")]
        [Produces("application/json")]
        public IActionResult findAll(int movieId)
        {
            try
            {
                return Ok(new
                {
                    Status = ratingService.findAll(movieId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("findAllStatus")]
        [Produces("application/json")]
        public IActionResult findAll()
        {
            try
            {
                return Ok(new
                {
                    Status = ratingService.findAll()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("findAllByMovie/{movieId}")]
        [Produces("application/json")]
        public IActionResult findAllByMovie(int movieId)
        {
            try
            {
                return Ok(new
                {
                    Status = ratingService.findAll(movieId, true)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("average/{movieId}")]
        [Produces("application/json")]
        public IActionResult avgByMovieId(int movieId)
        {
            try
            {
                return Ok(new
                {
                    Status = ratingService.avgByMovieId(movieId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
