namespace Application.DTOs
{
    public class MatchDto
    {
        public required string AnimalOneId { get; set; }

        public required string AnimalTwoId { get; set; }

        public required bool SwipedRight { get; set; }
    }
}
