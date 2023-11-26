namespace Application.Service.Interfaces
{
    public interface IMessageService
    {
        Task SaveMessage(string matchId, string animalId, string message);
    }
}
