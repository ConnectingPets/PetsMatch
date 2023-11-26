namespace API.Hubs
{
    using Application.Exceptions;
    using Application.Service.Interfaces;
    using Microsoft.AspNetCore.SignalR;
    using static Common.ExceptionMessages.Entity;

    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;

        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task SendMessage(string matchId, string animalId, string message)
        {
            try
            {
                await this.messageService.SaveMessage(matchId, animalId, message);
                await base.Clients.Group(matchId).SendAsync(animalId, message);
            }
            catch (InvalidGuidFormatException ex)
            {
                await base.Clients.Caller.SendAsync("InvalidGuidFormat", ex.Message);
            }
            catch (MatchNotFoundException ex)
            {
                await base.Clients.Caller.SendAsync("MatchNotFound", ex.Message);
            }
            catch (AnimalNotFoundException ex)
            {
                await base.Clients.Caller.SendAsync("AnimalNotFound", ex.Message);
            }
            catch
            {
                await base.Clients.Caller.SendAsync("SendMessageFailed", InternalServerError);
            }
        }
    }
}
