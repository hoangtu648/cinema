using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;
using Twilio.Types;
using Twilio;
using cinema.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace cinema.Controllers
{
    [Route("api/movie")]
    public class MovieController : Controller
    {
        public MovieService movieService;
        public IWebHostEnvironment webHostEnvironment;
        public MovieController(MovieService _movieService, IWebHostEnvironment _webHostEnvironment)
        {
            this.movieService = _movieService;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet("findAll")]
        [Produces("application/json")]
        public IActionResult findAll(string date, int cinemaId)
        {
          
            try
            {
                return Ok(movieService.findAll(true, date, cinemaId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findAllByStatus")]
        [Produces("application/json")]
        public IActionResult findAll()
        {
            try
            {
                return Ok(movieService.findAll(true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findMovieById/{id}")]
        [Produces("application/json")]
        public IActionResult findMovieById(int id)
        {
            try
            {
                return Ok(movieService.findById(id));
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
                return Ok(movieService.findMovie(true, date, cinemaId, movieId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        [Produces("application/json")]
       
        public IActionResult create(IFormFile photo, string movie)
        {
            
            
            try
            {
                var fileName = FileHelper.generateFileName(photo.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
           
                var movieObject = JsonConvert.DeserializeObject<Movie>(movie,
                    new IsoDateTimeConverter
                    {
                        DateTimeFormat = "dd/MM/yyyy"
                    });
                movieObject.Status = true;
                movieObject.Photo = "http://localhost:5113/images/" + fileName;
                return Ok(new
                {
                    fileName = "http://localhost:5113/images/" + fileName,
                    Status = movieService.create(movieObject)
                    
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
            var movie = movieService.findById1(id);
            movie.Status = false;
            try
            {
                return Ok(new
                {
                    Status = movieService.update(movie)
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
        public IActionResult edit([FromBody] Movie movie)

        {
            movie.Status = true;
            try
            {
                return Ok(new
                {
                    Status = movieService.update(movie)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
