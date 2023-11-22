namespace Domain
{
    using Domain.Enum;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static Common.EntityValidationConstants.User;
    using static Common.ExceptionMessages.User;

    [Comment("user table")]
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            base.Id = Guid.NewGuid();
            this.UsersPassions = new HashSet<UserPassion>();
            this.Animals = new HashSet<Animal>();
        }

        [Comment("user name")]
        [StringLength(NameMaxLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        [Comment("user description")]
        [StringLength(DescriptionMaxLength, ErrorMessage = InvalidDescriptionLength)]
        public string? Description { get; set; }

        [Comment("user age")]
        [Range(typeof(int), AgeMinValue, AgeMaxValue)]
        public required int? Age { get; set; }

        [Comment("user education")]
        [StringLength(EducationMaxLength, ErrorMessage = InvalidEducationLength)]
        public required string? Education { get; set; }

        [Comment("user photo")]
        public byte[]? Photo { get; set; }

        [Comment("user job title")]
        [StringLength(JobTitleMaxLength, ErrorMessage = InvalidJobTitleLength)]
        public required string? JobTitle { get; set; }

        [Comment("user gender")]
        public required Gender? Gender { get; set; }

        [Comment("user address")]
        [StringLength(AddressMaxLength, ErrorMessage = InvalidAddressLength)]
        public required string? Address { get; set; }

        [Comment("user city")]
        [StringLength(CityMaxLength, ErrorMessage = InvalidCityLength)]
        public required string? City { get; set; }

        public ICollection<UserPassion> UsersPassions { get; set; } = null!;

        public ICollection<Animal> Animals { get; set; } = null!;
    }
}