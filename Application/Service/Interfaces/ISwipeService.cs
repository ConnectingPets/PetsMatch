﻿namespace Application.Service.Interfaces
{
    using Application.DTOs.Swipe;
    using Application.Response;

    public interface ISwipeService
    {
        Task<Result<bool>> Swipe(string swiperAnimalId, string swipeeAnimalId, bool swipedRight);

        Task<Result<IEnumerable<AnimalToSwipeDto>>> GetAnimalsToSwipe(string userId);
    }
}
