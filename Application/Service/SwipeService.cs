﻿namespace Application.Service
{
    using System;
    using System.Threading.Tasks;

    using MediatR;

    using Application.Service.Interfaces;
    using static Application.Swipe.SwipeUser;
    using static Application.Matches.MatchAnimal;

    public class SwipeService : ISwipeService
    {
        private readonly IMediator mediator;

        public SwipeService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Swipe(Guid swiperAnimalId, Guid swipeeAnimalId, bool swipedRight)
        {
            bool isMatch = await this.mediator.Send(new SwipeUserCommand
            {
                SwiperAnimalId = swiperAnimalId,
                SwipedRight = swipedRight,
                SwipeeAnimalId = swipeeAnimalId
            });

            if (isMatch)
            {
                await this.mediator.Send(new MatchAnimalCommand
                {
                    AnimalOneId = swiperAnimalId,
                    AnimalTwoId = swipeeAnimalId,
                });
            }
            
            return isMatch;
        }
    }
}
