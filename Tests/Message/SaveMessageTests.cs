namespace Tests.Message
{
    using System.Linq.Expressions;

    using Moq;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Application.Message.SaveMessage;

    [TestFixture]
    public class SaveMessageTests
    {
        private Mock<IRepository> repositoryMock;
        private SaveMessageHandler handler;
        private Animal animal;
        private SaveMessageCommand command;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new SaveMessageHandler(repositoryMock.Object);
            animal = new Animal()
            {
                Age = 1,
                AnimalStatus = AnimalStatus.ForSwiping,
                BreedId = 1,
                Gender = Gender.Male,
                HealthStatus = HealthStatus.Vaccinated,
                IsEducated = false,
                IsHavingValidDocuments = false,
                Name = "Test",
                OwnerId = Guid.Parse("0e48736b-7b3a-41cb-ac14-e22e3d77571b"),
            };
            command = new SaveMessageCommand()
            {
                UserId = "0e48736b-7b3a-41cb-ac14-e22e3d77571b",
                AnimalId = animal.AnimalId.ToString(),
                Content = "Hello test",
                MatchId = Guid.NewGuid().ToString(),

            };
        }

        [Test]
        public async Task Handle_ShouldSaveMessage_WhenGivingCorrectData()
        {
            SetUpReturningAnimal(repositoryMock, animal);
            SetUpAnyAsyncReturnsTrue<Domain.Match>(repositoryMock);
            SetUpAnyAsyncReturnsTrue<User>(repositoryMock);

            repositoryMock.Setup(r => r.
            AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                 .ReturnsAsync(true);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Message>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenAnimalNotExist()
        {
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not exist! Please select an existing one", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenMatchIsNotFound()
        {
            SetUpReturningAnimal(repositoryMock, animal);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The animals are not matched", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenUserIsNotFound()
        {
            SetUpReturningAnimal(repositoryMock, animal);
            SetUpAnyAsyncReturnsTrue<Domain.Match>(repositoryMock);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("The user is not found", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnsError_WhenUserIsNotOwner()
        {
            SetUpReturningAnimal(repositoryMock, animal);
            SetUpAnyAsyncReturnsTrue<Domain.Match>(repositoryMock);
            SetUpAnyAsyncReturnsTrue<User>(repositoryMock);

            command.UserId = Guid.NewGuid().ToString();

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("This pet does not belong to you", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_FailedToCreateMessage_ReturnsFailureResult()
        {
            SetUpReturningAnimal(repositoryMock, animal);
            SetUpAnyAsyncReturnsTrue<Domain.Match>(repositoryMock);
            SetUpAnyAsyncReturnsTrue<User>(repositoryMock);

            repositoryMock.Setup(r => r.SaveChangesAsync()).Throws(new Exception("Simulating save failure"));

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to send the message", result.ErrorMessage);
        }

        private static void SetUpReturningAnimal(
            Mock<IRepository> repositoryMock,
            Animal animal)
        {
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>()))
                .ReturnsAsync(animal);
        }

        private static void SetUpAnyAsyncReturnsTrue<T>(
            Mock<IRepository> repositoryMock) where T : class
        {
            repositoryMock.Setup(r => r.
            AnyAsync(It.IsAny<Expression<Func<T, bool>>>())).
                ReturnsAsync(true);
        }
    }
}
