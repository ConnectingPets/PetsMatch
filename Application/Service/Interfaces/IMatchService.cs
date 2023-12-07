namespace Application.Service.Interfaces
{
    using Application.DTOs.Match;
    using Application.Response;
    using MediatR;

    public interface IMatchService 
    {
        Task<Result<IEnumerable<AnimalMatchDto>>> GetAnimalMatches(string animalId);

        Task<Result<Unit>> UnMatch(string animalOneId, string animalTwoId);

        Task<Result<Unit>> Match(string animalOneId, string animalTwoId);
    }
}
