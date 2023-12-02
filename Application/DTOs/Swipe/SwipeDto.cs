namespace Application.DTOs.Swipe
{
    using System.ComponentModel.DataAnnotations;

    public class SwipeDto
    {
        [Required]
        public required string SwiperAnimalId { get; set; }

        [Required]
        public required string SwipeeAnimalId { get; set; }

        [Required]
        public required bool SwipedRight { get; set; }
    }
}
