using cinema.Helpers;
using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace cinema.Controllers
{
    [Route("api/cinema")]
    public class CinemaController : Controller
    {
        public CinemaService cinemaService;
  
        public CinemaController(CinemaService _cinemaService)
        {
            cinemaService = _cinemaService;
          
        }
        [HttpGet("findAll")]
        [Produces("application/json")]

        public IActionResult findAll()
        {
            
            try
            {
                return Ok(cinemaService.findAll());
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}
