namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MediatR;

    using Application.DTOs.Message;
    using Application.Service.Interfaces;
    using Application.Response;
    using API.Infrastructure;

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
            Result<DateTime> result = await this.messageService.SaveMessage(
                saveMessageDto.MatchId,
                saveMessageDto.AnimalId,
                saveMessageDto.Content,
                User.GetById());

            return Ok(result);
        }

        [Route("/chatHistory")]
        [HttpGet]
        public async Task<ActionResult> ChatHistory([FromQuery] string matchId)
        {
            Result<IEnumerable<ChatMessageDto>> result = await this.messageService.GetChatHistory(
                matchId,
                User.GetById());

            return Ok(result);
        }
    }
}