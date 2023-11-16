namespace Application.DTOs
{
    public class SwipeDto
    {
        public required string SwiperAnimalId { get; set; }

        public required string SwipeeAnimalId { get; set; }

        public required bool SwipedRight { get; set; }
    }
}
