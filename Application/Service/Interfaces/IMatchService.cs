namespace Application.Service.Interfaces
{
    using Application.DTOs.Match;
    using Application.Response;
    using MediatR;

    public interface IMatchService 
    {
        Task<Result<IEnumerable<AnimalMatchDto>>> GetAnimalMatches(string animalId, string userId);

        Task<Result<Unit>> UnMatch(string animalOneId, string animalTwoId, string userId);

        Task<Result<Unit>> Match(string animalOneId, string animalTwoId, string userId);
    }
}
