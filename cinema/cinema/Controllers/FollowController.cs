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
    [Route("api/follow")]
    public class FollowController : Controller
    {
        public FollowService followService;
  
        public FollowController(FollowService _followService)
        {
            followService = _followService;
          
        }
        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult findAll([FromBody] Follow follow)
        {
            
            try
            {
                return Ok(new
                {
                    Status = followService.create(follow)
                });
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("findById/{id}")]
        [Produces("application/json")]
     
        public IActionResult findById(int id)
        {

            try
            {
                return Ok(followService.findById(id));
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
                return Ok(followService.findAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
