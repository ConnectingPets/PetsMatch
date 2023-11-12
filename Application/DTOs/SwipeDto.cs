namespace Application.DTOs
{
    public class SwipeDto
    {
        public required Guid SwiperAnimalId { get; set; }

        public required Guid SwipeeAnimalId { get; set; }

        public required bool SwipedRight { get; set; }
    }
}
