namespace Application.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;

    using Application.Service.Interfaces;
    using static Application.Match.AnimalMatches;
    using static Application.Match.UnMatchAnimal;
    using static Application.Matches.MatchAnimal;
    using Application.DTOs.Match;

    public class MatchService : IMatchService
    {
        private readonly IMediator mediator;

        public MatchService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IEnumerable<AnimalMatchDto>> GetAnimalMatches(string animalId)
            => await this.mediator.Send(new AnimalMatchesQuery
            {
                AnimalId = animalId
            });

        public async Task Match(string animalOneId, string animalTwoId)
            => await this.mediator.Send(new MatchAnimalCommand
            {
                AnimalOneId = animalOneId,
                AnimalTwoId = animalTwoId
            });

        public async Task UnMatch(string animalOneId, string animalTwoId)
            => await this.mediator.Send(new UnMatchAnimalCommand
            {
                AnimalOneId = animalOneId,
                AnimalTwoId = animalTwoId
            });
    }
}
