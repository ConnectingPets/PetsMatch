namespace Application.Service.Interfaces
{
    using Application.DTOs;

    public interface IMatchService 
    {
        Task<IEnumerable<UserMatchDto>> GetAnimalMatches(Guid animalId);

        Task UnMatch(Guid animalOneId, Guid animalTwoId);
    }
}
