namespace Application.Service.Interfaces
{
    public interface ISwipeService
    {
        Task<bool> Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight);
    }
}
