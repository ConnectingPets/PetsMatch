namespace Application.Service.Interfaces
{
    public interface ISwipeService
    {
        Task<bool> Swipe(Guid swiperAnimalId, Guid swipeeAnimalId, bool swipedRight);
    }
}
