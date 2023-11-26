namespace Application.DTOs.Animal
{
    using System.Collections.Generic;
    using Application.DTOs.Breed;

    public class ShowAnimalToAddDto
    {
        public ShowAnimalToAddDto()
        {
            Breeds = new HashSet<BreedDto>();
            AnimalCategories = new HashSet<AnimalCategoryDto>();
        }

        public IEnumerable<BreedDto> Breeds { get; set; } = null!;

        public IEnumerable<AnimalCategoryDto> AnimalCategories { get; set; } = null!;
    }
}
