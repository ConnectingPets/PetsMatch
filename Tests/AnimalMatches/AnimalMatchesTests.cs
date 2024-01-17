namespace Tests.AnimalMatches
{
    using System.Linq.Expressions;

    using Moq;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Match.AnimalMatches;

    [TestFixture]
    public class AnimalMatchesTests
    {
        private Mock<IRepository> repositoryMock;
        private AnimalMatchesHandler handler;
        private Animal animal;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new AnimalMatchesHandler(repositoryMock.Object);
            animal = new Animal
            {
                AnimalId = Guid.Parse("db12895b-9021-4dc6-8777-decd565b67a2"),
                Name = "Pet1",
                Age = 1,
                AnimalStatus = AnimalStatus.ForSale,
                BreedId = 2,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = true,
                IsHavingValidDocuments = true,
                OwnerId = Guid.Parse("62c26169-5cf5-47d5-b693-230be0cb35c7"),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                         Id = "Id",
                         IsMain = true,
                         Url = "URL"
                    }
                }
            };
        }

        [Test]
        public async Task Handle_ValidAnimalId_ReturnsAnimalMatch()
        {
            var query = new AnimalMatchesQuery
            {
                AnimalId = "db12895b-9021-4dc6-8777-decd565b67a2",
                UserId = "62c26169-5cf5-47d5-b693-230be0cb35c7"
            };

            var animal2 = new Animal
            {
                AnimalId =
                Guid.Parse("9e2bb42a-a9eb-4876-b9a1-f1f1d565dd41"),
                Name = "Match1",
                Age = 1,
                AnimalStatus = AnimalStatus.ForSale,
                BreedId = 2,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = true,
                IsHavingValidDocuments = true,
                OwnerId =
                Guid.Parse("2041546c-86ec-49c4-b88c-b2d95f09837c"),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                         Id = "Id",
                         IsMain = true,
                         Url = "URL"
                    }
                }
            };

            var match1 = new Domain.Match
            {
                MatchId = Guid.NewGuid(),
                MatchOn = DateTime.UtcNow,
                AnimalMatches = new List<AnimalMatch>
                {
                   new AnimalMatch
                   {
                       AnimalId = animal.AnimalId,
                       Animal = animal
                   }
                }
            };

            var match2 = new Domain.Match
            {
                MatchId = Guid.NewGuid(),
                MatchOn = DateTime.UtcNow,
                AnimalMatches = new List<AnimalMatch>
                {
                    new AnimalMatch
                    {
                        AnimalId = animal2.AnimalId,
                        Animal = animal2,
                    }
                }
            };

            animal.AnimalMatches = new List<AnimalMatch>
            {
                new AnimalMatch
                {
                    MatchId = match2.MatchId,
                    Match = match2,
                    AnimalId = animal2.AnimalId,
                    Animal = animal2
                }
            };

            var queryableAnimal = new List<Animal> { animal }.AsQueryable();
            var asyncEnumerableAnimal =
                new TestAsyncEnumerableEfCore<Animal>(queryableAnimal);
            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<Animal, bool>>>())).
                Returns(asyncEnumerableAnimal);

            SetUpReturningUser(repositoryMock);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Data.Count());
            Assert.AreEqual("Match1", result.Data.First().Name);
            Assert.AreEqual(result.Data.First().Photo.ToString(),"URL");
        }

        [Test]
        public async Task Handle_AnimalNotFound_ReturnsErrorResult()
        {
            var query = new AnimalMatchesQuery
            {
                AnimalId = "6a160c80-7571-4086-a2f7-fb87c5d2be95"
            };

            var queryableAnimal = new List<Animal>().AsQueryable();
            SetUpReturningAnimal(queryableAnimal, repositoryMock);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsErrorResult()
        {
            var query = new AnimalMatchesQuery()
            {
                UserId = Guid.NewGuid().ToString()
            };

            var queryableAnimal =
                new List<Animal>() { animal }.AsQueryable();
            SetUpReturningAnimal(queryableAnimal, repositoryMock);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_NotOwner_ReturnsErrorResult()
        {
            var query = new AnimalMatchesQuery()
            {
                UserId = Guid.NewGuid().ToString()
            };

            var queryableAnimal =
                new List<Animal>() { animal }.AsQueryable();
            SetUpReturningAnimal(queryableAnimal, repositoryMock);
            SetUpReturningUser(repositoryMock);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        private static void SetUpReturningAnimal(
            IQueryable<Animal> queryableAnimal,
            Mock<IRepository> repositoryMock)
        {
            var asyncEnumerableAnimal =
                new TestAsyncEnumerableEfCore<Animal>(queryableAnimal);
            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<Animal, bool>>>())).
                Returns(asyncEnumerableAnimal);
        }

        private static void SetUpReturningUser(
            Mock<IRepository> repositoryMock)
        {
            repositoryMock.
                Setup(r => r.
                AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync(true);
        }
    }
}
