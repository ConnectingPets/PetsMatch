namespace Persistence.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Collections.Generic;

    public class AnimalCategoryEntityConfiguration : IEntityTypeConfiguration<AnimalCategory>
    {
        public void Configure(EntityTypeBuilder<AnimalCategory> builder)
        {
            IEnumerable<AnimalCategory> categories = new List<AnimalCategory>
            {
                new AnimalCategory
                {
                    AnimalCategoryId = 1,
                    Name = "Dog"
                },
                new AnimalCategory
                {
                    AnimalCategoryId = 2,
                    Name = "Cat"
                },
                new AnimalCategory
                {
                    AnimalCategoryId = 3,
                    Name = "Rabbit"
                }
            };

            builder.HasData(categories);
        }
    }
}
