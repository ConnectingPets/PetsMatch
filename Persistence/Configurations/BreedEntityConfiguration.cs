namespace Persistence.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BreedEntityConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            IEnumerable<Breed> breeds = new List<Breed>
            {
                new Breed
                {
                    BreedId = 1,
                    Name = "Persian Cat",
                    CategoryId = 2
                },
                new Breed
                {
                    BreedId = 2,
                    Name = "Bengal Cat",
                    CategoryId = 2
                },
                new Breed
                {
                    BreedId = 3,
                    Name = "American Rabbit",
                    CategoryId = 3
                },
                new Breed
                {
                    BreedId = 4,
                    Name = "Pitbull",
                    CategoryId = 1
                },
                new Breed
                {
                    BreedId = 5,
                    Name = "BullDog",
                    CategoryId = 1
                }
            };

            builder.HasData(breeds);
        }
    }
}
