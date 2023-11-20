namespace Application.DTOs
{
    using System.Collections.Generic;
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
