﻿namespace Application.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;

    using Application.DTOs;
    using Application.Service.Interfaces;
    using static Application.Match.AnimalMatches;
    using static Application.Match.UnMatchAnimal;
    using static Application.Matches.MatchAnimal;

    public class MatchService : IMatchService
    {
        private readonly IMediator mediator;

        public MatchService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IEnumerable<AnimalMatchDto>> GetAnimalMatches(Guid animalId)
            => await this.mediator.Send(new AnimalMatchesQuery
            {
                AnimalId = animalId
            });

        public async Task Match(Guid animalOneId, Guid animalTwoId, bool swipedRight)
            => await this.mediator.Send(new MatchAnimalCommand
            {
                AnimalOneId = animalOneId,
                AnimalTwoId = animalTwoId,
                SwipedRight = swipedRight
            });

        public async Task UnMatch(Guid animalOneId, Guid animalTwoId)
            => await this.mediator.Send(new UnMatchAnimalCommand
            {
                AnimalOneId = animalOneId,
                AnimalTwoId = animalTwoId
            });
    }
}