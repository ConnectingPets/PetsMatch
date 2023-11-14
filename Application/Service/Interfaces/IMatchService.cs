namespace Application.Service.Interfaces
{
    using Application.DTOs;

    public interface IMatchService 
    {
        Task<IEnumerable<AnimalMatchDto>> GetAnimalMatches(Guid animalId);

        Task UnMatch(Guid animalOneId, Guid animalTwoId);

        Task Match(Guid animalOneId, Guid animalTwoId);
    }
}
