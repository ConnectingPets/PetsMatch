namespace Application.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;

    using Application.DTOs;
    using Application.Service.Interfaces;
    using static Application.Match.AnimalMatches;

    public class MatchService : IMatchService
    {
        private readonly IMediator mediator;

        public MatchService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IEnumerable<UserMatchDto>> GetAnimalMatches(Guid animalId)
            => await this.mediator.Send(new AnimalMatchesQuery
            {
                AnimalId = animalId
            });
    }
}
