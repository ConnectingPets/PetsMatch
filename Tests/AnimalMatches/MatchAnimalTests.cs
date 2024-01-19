namespace Tests.AnimalMatches
{
    using System;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    using Moq;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Matches.MatchAnimal;

    [TestFixture]
    public class MatchAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private MatchAnimalHandler handler;
        private MatchAnimalCommand command;
        private Animal animalOne;
        private Animal animalTwo;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new MatchAnimalHandler(repositoryMock.Object);
            command = new MatchAnimalCommand
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
        }

        [Test]
        public async Task Handle_ValidCommand_CreatesMatch()
        {
            var animalOneId =
                Guid.Parse("da1c3297-96d5-4478-b29e-455c0c3d84ce");
            var animalTwoId =
                Guid.Parse("a1572863-fb51-42f0-8f35-b22db1ad1454");
            var ownerId =
                Guid.Parse("03a55cc2-f093-44f7-acae-932b14e8efd6");

            var command = new MatchAnimalCommand
            {
                AnimalOneId = animalOneId.ToString(),
                AnimalTwoId = animalTwoId.ToString(),
                UserId = ownerId.ToString()
            };

            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            SetUpReturningMatch(repositoryMock);

            var result = await handler.Handle(command, CancellationToken.None);

            repositoryMock.
                Verify(r => r.AddAsync(It.IsAny<Domain.Match>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("The animals are successfully matched", result.SuccessMessage);
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
            SetUpReturningFirstAnimal(repositoryMock,command, animalOne);
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

            var result = 
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_IsPresentMatch_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            repositoryMock.Setup(r => r.
            AnyAsync(It.IsAny<Expression<Func<AnimalMatch, bool>>>())).
               ReturnsAsync(true);

            command.UserId = "03a55cc2-f093-44f7-acae-932b14e8efd6";

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("There is already a match between these animals", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_NotMatch_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);

            command.UserId = "03a55cc2-f093-44f7-acae-932b14e8efd6";

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The animals don't like each other", result.ErrorMessage);
        }
        [Test]
        public async Task Handle_SaveChanges_ReturnsErrorResult()
        {
            SetUpReturningFirstAnimal(repositoryMock, command, animalOne);
            SetUpReturningSecondAnimal(repositoryMock, command, animalTwo);
            SetUpReturningUser(repositoryMock);
            SetUpReturningMatch(repositoryMock);
            repositoryMock.Setup(r => r.SaveChangesAsync())
                .Throws(new Exception());

            command.UserId = "03a55cc2-f093-44f7-acae-932b14e8efd6";

            var result =
                await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to match the animals", result.ErrorMessage);
        }

        private static void SetUpReturningFirstAnimal(
            Mock<IRepository> repositoryMock,
            MatchAnimalCommand command,
            Animal animalOne)
        {
            repositoryMock.Setup(r => r.
            FirstOrDefaultAsync<Animal>(animal => animal.AnimalId.ToString() == command.AnimalOneId.ToLower()))
                .ReturnsAsync(animalOne);
        }

        private static void SetUpReturningSecondAnimal(
           Mock<IRepository> repositoryMock,
           MatchAnimalCommand command,
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
            Mock<IRepository> repositoryMock)
        {
            repositoryMock.Setup(r => r.
            CountAsync(It.IsAny<Expression<Func<Swipe, bool>>>()))
                .ReturnsAsync(2);
        }
    }
}
