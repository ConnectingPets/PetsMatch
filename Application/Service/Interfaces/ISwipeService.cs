namespace Application.Service.Interfaces
{
    using Application.DTOs.Swipe;

    public interface ISwipeService
    {
        Task<bool> Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight);

        Task<IEnumerable<AnimalToSwipeDto>> GetAnimalsToSwipe(string userId);
    }
}
