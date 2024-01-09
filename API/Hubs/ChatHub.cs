namespace API.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string matchId, string animalId, string message, DateTime date)
        {
            await base.Groups.AddToGroupAsync(Context.ConnectionId, matchId);
            await base.Clients.Group(matchId).SendAsync("ReceiveMessage", animalId, message, date);
        }
    }
}
