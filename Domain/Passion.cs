namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Passion;
    using static Common.ExceptionMessages.Passion;

    [Comment("passion table")]
    public class Passion
    {
        public Passion()
        {
            this.UsersPassions = new HashSet<UserPassion>();
        }

        [Comment("passion id")]
        [Key]
        public int PassionId { get; set; }

        [Comment("passion name")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        public ICollection<UserPassion> UsersPassions { get; set; } = null!;
    }
}
