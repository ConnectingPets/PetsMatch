namespace API.Hubs
{
    using Application.Service.Interfaces;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string matchId, string animalId, string message)
        {
            await base.Groups.AddToGroupAsync(Context.ConnectionId, matchId);
            await base.Clients.Group(matchId).SendAsync(animalId, message);
        }
    }
}
