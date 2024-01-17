namespace Tests.AnimalMatches
{
    using Application.Match;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Domain;
    using static Application.Match.UnMatchAnimal;
    using System.Linq.Expressions;
    using Domain.Enum;
    using static Application.Matches.MatchAnimal;
    using MockQueryable.EntityFrameworkCore;

    [TestFixture]
    public class UnMatchAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private UnMatchAnimalHandler handler;
        private UnMatchAnimalCommand command;
        private Animal animalOne;
        private Animal animalTwo;
        private Domain.Match existingMatch;
        private AnimalMatch animalMatch;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new UnMatchAnimalHandler(repositoryMock.Object);
            command = new UnMatchAnimalCommand
            {
                AnimalOneId = "db2b950a-4940-48c0-8532-3defcad7a6bf",
                AnimalTwoId = "d2b9a654-ea01-4b7a-a6f9-0c2481133c06",
                UserId = "aa5f4257-3ae0-46a2-80ac-7755bed9f4e4"
            };
            animalOne = new Animal()
            {
                AnimalId = Guid.Parse("da1c3297-96d5-4478-b29e-455c0c3d84ce"),
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Female,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = true,
                IsHavingValidDocuments = true,
                Name = "AnimalOne",
                OwnerId = Guid.Parse("03a55cc2-f093-44f7-acae-932b14e8efd6")
            };
            animalTwo = new Animal()
            {
                AnimalId = Guid.Parse("a1572863-fb51-42f0-8f35-b22db1ad1454"),
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Female,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = true,
                IsHavingValidDocuments = true,
                Name = "AnimalTwo",
                OwnerId = Guid.NewGuid()
            };
            existingMatch = new Domain.Match
            {
                MatchId = Guid.NewGuid(),
                MatchOn = DateTime.UtcNow,
                AnimalMatches = new[]
                {
                new AnimalMatch { AnimalId = Guid.Parse(command.AnimalOneId) },
                new AnimalMatch { AnimalId = Guid.Parse(command.AnimalTwoId) }
                }
            };
            animalMatch = new AnimalMatch()
            {
                Match = existingMatch,
                MatchId = Guid.NewGuid(),
                Animal = animalOne,
                AnimalId = animalOne.AnimalId
            };
        }

        [Test]
        public async Task Handle_ValidCommand_DeletesMatch()
        {
            var command = new UnMatchAnimalCommand
            {
                AnimalOneId = "da1c3297-96d5-4478-b29e-455c0c3d84ce",
                AnimalTwoId = "a1572863-fb51-42f0-8f35-b22db1ad1454",
                UserId = "03a55cc2-f093-44f7-acae-932b14e8efd6"
            };

            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            SetUpReturningMatch(repositoryMock, animalMatch);

            var result = await handler.Handle(command, CancellationToken.None);

            repositoryMock.Verify(r => r.DeleteRange(It.IsAny<Expression<Func<Swipe,bool>>>()), Times.Once);
            repositoryMock.Verify(r => r.
            Delete(It.IsAny<Domain.Match>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("The animals are successfully unmatched", result.SuccessMessage);
        }

        [Test]
        public async Task Handle_InvalidAnimalOneId_ReturnsErrorResult()
        {
            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_InvalidAnimalTwoId_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalOne);

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_SameAnimal_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            command.AnimalTwoId = command.AnimalOneId;

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The animal is matching on itself", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_NotOwner_ReturnsErrorResult()
        {
            animalOne.OwnerId = Guid.NewGuid();
            animalTwo.OwnerId = Guid.NewGuid();
            command.UserId = Guid.NewGuid().ToString();

            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            SetUpReturningMatch(repositoryMock, animalMatch);

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_MatchNotFound_ReturnsErrorResult()
        {
            command.UserId = "03a55cc2-f093-44f7-acae-932b14e8efd6";

            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);

            var queryable =
               new List<AnimalMatch> { }.AsQueryable();
            var asyncEnumerable =
               new TestAsyncEnumerableEfCore<AnimalMatch>(queryable);
            repositoryMock.Setup(r => r.
            All(It.IsAny<Expression<Func<AnimalMatch, bool>>>()))
                .Returns(asyncEnumerable);

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The animals are not matched", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_SaveChanges_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            SetUpReturningMatch(repositoryMock,animalMatch);
            repositoryMock.Setup(r => r.SaveChangesAsync())
                .Throws(new Exception());

            command.UserId = "03a55cc2-f093-44f7-acae-932b14e8efd6";

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to unmatch the animals", result.ErrorMessage);
        }

        private static void SetUpReturningFirstAnimal(
           Mock<IRepository> repositoryMock,
           UnMatchAnimalCommand command,
           Animal animalOne)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync<Animal>(animal => animal.AnimalId.ToString() == command.AnimalOneId.ToLower())).ReturnsAsync(animalOne);
        }

        private static void SetUpReturningSecondAnimal(
          Mock<IRepository> repositoryMock,
          UnMatchAnimalCommand command,
          Animal animalTwo)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync<Animal>(animal => animal.AnimalId.ToString() == command.AnimalTwoId.ToLower()))
                .ReturnsAsync(animalTwo);
        }

        private static void SetUpReturningUser(
             Mock<IRepository> repositoryMock)
        {
            repositoryMock.Setup(r => r.
            AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).
                ReturnsAsync(true);
        }

        private static void SetUpReturningMatch(
            Mock<IRepository> repositoryMock,
            AnimalMatch animalMatch)
        {
            var queryable =
                new List<AnimalMatch> { animalMatch }.AsQueryable();
            var asyncEnumerable =
               new TestAsyncEnumerableEfCore<AnimalMatch>(queryable);
            repositoryMock.Setup(r => r.
            All(It.IsAny<Expression<Func<AnimalMatch, bool>>>()))
                .Returns(asyncEnumerable);
        }
    }
}
