namespace Application.Service.Interfaces
{
    using Application.DTOs;

    public interface IMatchService 
    {
        Task<IEnumerable<AnimalMatchDto>> GetAnimalMatches(string animalId);

        Task UnMatch(string animalOneId, string animalTwoId);

        Task Match(string animalOneId, string animalTwoId);
    }
}
