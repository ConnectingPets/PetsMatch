namespace Application.DTOs.Animal
{
    using System.ComponentModel.DataAnnotations;

    using Domain.Enum;
    using static Common.EntityValidationConstants.Animal;
    using static Common.ExceptionMessages.Animal;

    public class EditAnimalDto 
    {
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }


        [StringLength(DescriptionMaxLength, ErrorMessage = InvalidDescriptionLength)]
        public string? Description { get; set; }

        [Range(typeof(int), AgeMinValue, AgeMaxValue)]
        public required int Age { get; set; }

        public DateTime? BirthDate { get; set; }

        public required bool IsEducated { get; set; }

        public required HealthStatus HealthStatus { get; set; }

        public required Gender Gender { get; set; }

        public string? SocialMedia { get; set; }

        [Range(typeof(double), WeightMinValue, WeightMaxValue)]
        public double? Weight { get; set; }

        public required bool IsHavingValidDocuments { get; set; }

        public required int BreedId { get; set; }
    }
}
