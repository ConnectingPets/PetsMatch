namespace Application.Service
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;

    using Application.DTOs.Swipe;
    using Application.Service.Interfaces;
    using Application.Response;

    using static Application.Swipe.SwipeAnimal;
    using static Application.Swipe.AnimalsToSwipe;

    public class SwipeService : ISwipeService
    {
        private readonly IMediator mediator;

        public SwipeService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result<IEnumerable<AnimalToSwipeDto>>> GetAnimalsToSwipe(string animalId, string userId)
            => await this.mediator.Send(new AnimalsToSwipeQuery
            {
                UserId = userId,
                AnimalId = animalId
            });

        public async Task<Result<bool>> Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight, string userId)
            => await this.mediator.Send(new SwipeAnimalCommand
            {
                SwiperAnimalId = swiperAnimalId,
                SwipeeAnimalId = swipeeAnimalId,
                SwipedRight = swipedRight,
                UserId = userId
            });
    }
}
