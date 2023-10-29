namespace Domain
{
    //Here we will store the data about the swipes.
    public class Swipe
    {
        public int SwipeID { get; set; }
        public int UserID { get; set; }
        public int SwipedUserID { get; set; }

        // Like/Dislike
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public User User { get; set; }
    }
}