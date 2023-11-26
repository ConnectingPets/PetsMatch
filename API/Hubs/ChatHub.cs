namespace API.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string matchId, string animalId, string message)
        {
            await base.Clients.Group(matchId).SendAsync(animalId, message);
        }
    }
}
