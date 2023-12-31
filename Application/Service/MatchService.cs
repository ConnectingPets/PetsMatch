﻿namespace Application.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;

    using Application.Response;
    using Application.DTOs.Match;
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

        public async Task<Result<IEnumerable<AnimalMatchDto>>> GetAnimalMatches(string animalId, string userId)
            => await this.mediator.Send(new AnimalMatchesQuery
            {
                AnimalId = animalId,
                UserId = userId
            });

        public async Task<Result<Unit>> Match(string animalOneId, string animalTwoId, string userId)
            => await this.mediator.Send(new MatchAnimalCommand
            {
                AnimalOneId = animalOneId,
                AnimalTwoId = animalTwoId,
                UserId = userId
            });

        public async Task<Result<Unit>> UnMatch(string animalOneId, string animalTwoId, string userId)
            => await this.mediator.Send(new UnMatchAnimalCommand
            {
                AnimalOneId = animalOneId,
                AnimalTwoId = animalTwoId,
                UserId = userId
            });
    }
}
