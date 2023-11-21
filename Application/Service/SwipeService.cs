namespace Application.Service
{
    using System.Threading.Tasks;

    using MediatR;

    using Application.Service.Interfaces;
    using static Application.Swipe.SwipeAnimal;

    public class SwipeService : ISwipeService
    {
        private readonly IMediator mediator;

        public SwipeService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight)
            => await this.mediator.Send(new SwipeAnimalCommand
            {
                SwiperAnimalId = swiperAnimalId,
                SwipeeAnimalId = swipeeAnimalId,
                SwipedRight = swipedRight
            });
    }
}
