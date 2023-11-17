namespace Application.Service.Interfaces
{
    public interface ISwipeService
    {
        Task Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight);
    }
}
