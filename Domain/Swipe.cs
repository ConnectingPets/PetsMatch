namespace Domain
{
    using Microsoft.EntityFrameworkCore;

    [Comment("swipe table")]
    public class Swipe
    {
        public int SwipeID { get; set; }
        //public int UserID { get; set; }
        [Comment("swiper animal id")]
        public required int SwiperAnimalId { get; set; }

        [Comment("swipee animal id")]
        public required int SwipeeAnimalId { get; set; }

        // Like/Dislike
        [Comment("it stores of the swipe is right")]
        public required bool SwipedRight { get; set; }

        [Comment("timestamp when the swipe is made")]
        public DateTime SwipedOn { get; set; }
    }
}