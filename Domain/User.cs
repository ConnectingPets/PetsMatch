namespace Domain
{
    using Domain.Enum;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    [Comment("user table")]
    public class User : IdentityUser
    {
        [Comment("user name")]
        public required string Name { get; set; }

        [Comment("user description")]
        public string? Description { get; set; }

        [Comment("user age")]
        public required int Age { get; set; }

        [Comment("user education")]
        public required string Education { get; set; }

        [Comment("user photo")]
        public byte[]? Photo { get; set; }

        [Comment("user job title")]
        public required string JobTitle { get; set; }

        [Comment("user gender")]
        public required Gender Gender { get; set; }

        [Comment("user address")]
        public required string Address { get; set; }

        [Comment("user city")]
        public required string City { get; set; }

        public ICollection<Swipe> Swipes { get; set; }
    }
}