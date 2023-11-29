namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Application.DTOs.Message;
    using Application.Service.Interfaces;
    using static Common.ExceptionMessages.Entity;
    using Application.Exceptions.Entity;
    using Application.Exceptions.Animal;
    using Application.Exceptions.Match;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [Route("/sendMessage")]
        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] SaveMessageDto saveMessageDto)
        {
            try
            {
                await this.messageService.SaveMessage(
                    saveMessageDto.MatchId,
                    saveMessageDto.AnimalId,
                    saveMessageDto.Content);
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (MatchNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok();
        }

        [Route("/chatHistory")]
        [HttpGet]
        public async Task<ActionResult> ChatHistory([FromQuery] string matchId)
        {
            IEnumerable<ChatMessageDto> messages;
            try
            {
                messages = await this.messageService.GetChatHistory(matchId);
            }
            catch (MatchNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok(messages);
        }
    }
}