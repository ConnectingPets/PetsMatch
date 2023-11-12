namespace Application.Service.Interfaces
{
    public interface ISwipeService
    {
        Task Swipe(Guid swiperAnimalId, Guid swipeeAnimalId, bool swipedRight);
    }
}
