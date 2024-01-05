namespace Application.Service.Interfaces
{
    using Application.DTOs.Swipe;
    using Application.Response;

    public interface ISwipeService
    {
        Task<Result<bool>> Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight, string userId);

        Task<Result<IEnumerable<AnimalToSwipeDto>>> GetAnimalsToSwipe(string animalId, string userId);
    }
}
