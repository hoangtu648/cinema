using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {
        public ChatService chatService;
        public ChatController(ChatService _chatService)
        {
            chatService = _chatService;
        }

        [HttpGet("findChatByAccountId/{accountId}")]
        [Produces("application/json")]
        public IActionResult findAll(int accountId)
        {
            try
            {
                return Ok(chatService.findChatByAccountId(accountId));
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("newChat")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult newChat([FromBody] Chat chat)
        {
            
            try
            {
                return Ok(new
                {
                    Status = chatService.newChat(chat),
                    Chat = chat
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listUser")]
        [Produces("application/json")]
        public IActionResult listUser()
        {
            try
            {
                return Ok(chatService.listUser());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
