namespace Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("swipe table")]
    public class Swipe
    {
        [Comment("swiper animal id")]
        public required int SwiperAnimalId { get; set; }

        [Comment("swiper animal")]
        [ForeignKey(nameof(SwiperAnimalId))]
        public Animal SwiperAnimal { get; set; } = null!;

        [Comment("swipee animal id")]
        public required int SwipeeAnimalId { get; set; }

        [Comment("swipee animal")]
        [ForeignKey(nameof(SwipeeAnimalId))]
        public Animal SwipeeAnimal { get; set; } = null!;

        // Like/Dislike
        [Comment("it stores of the swipe is right")]
        public required bool SwipedRight { get; set; }

        [Comment("timestamp when the swipe is made")]
        public DateTime SwipedOn { get; set; }
    }
}