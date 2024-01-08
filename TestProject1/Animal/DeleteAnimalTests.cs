﻿namespace Tests.Animal
{
    using System.Linq.Expressions;

    using Moq;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.Service.Interfaces;

    using static Application.Animal.DeleteAnimal;

    [TestFixture]
    public class DeleteAnimalTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            photoServiceMock = new Mock<IPhotoService>();
        }

        [Test]
        public async Task Handle_ValidCommandAndOwner_ReturnsSuccessResult()
        {
            var handler = new DeleteAnimalCommandHandler(repositoryMock.Object, photoServiceMock.Object);

            var command = new DeleteAnimalCommand
            {
                AnimalId = "1c9cd1de-dba5-46f7-837e-a316447e13c0",
                UserId = "3e973314-0946-4c02-a352-0d0f2032c19d"
            };

            var animal = new Animal
            {
                AnimalId = Guid.Parse("1c9cd1de-dba5-46f7-837e-a316447e13c0"),
                OwnerId = Guid.Parse("3e973314-0946-4c02-a352-0d0f2032c19d"),
                Name = "Pet1",
                Age = 1,
                AnimalStatus = AnimalStatus.ForSale,
                BreedId = 2,
                Gender = Gender.Female,
                IsEducated = false,
                IsHavingValidDocuments = false,
                HealthStatus = HealthStatus.Vaccinated
            };

            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Animal, bool>>>())).ReturnsAsync(animal);

            repositoryMock.Setup(r => r.GetById<Animal>(Guid.Parse(command.AnimalId)))
                .ReturnsAsync(animal);

            repositoryMock.Setup(r => r.DeleteAsync<Animal>(Guid.Parse(command.AnimalId)))
                .Returns(Task.CompletedTask);

            repositoryMock.Setup(r => r.DeleteRange(It.IsAny<Expression<Func<Swipe, bool>>>()));

            repositoryMock.Setup(r => r.All(It.IsAny<Expression<Func<AnimalMatch, bool>>>()))
                .Returns(MockDbSet(Array.Empty<AnimalMatch>()));

            repositoryMock.Setup(r => r.All(It.IsAny<Expression<Func<Message, bool>>>()))
                .Returns(MockDbSet(Array.Empty<Message>()));

            repositoryMock.Setup(r => r.DeleteRange(It.IsAny<Message[]>()));

            repositoryMock.Setup(r => r.DeleteRange(It.IsAny<AnimalMatch[]>()));

            repositoryMock.Setup(r => r.DeleteAsync<Domain.Match>(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            repositoryMock.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"You have successfully delete {animal.Name} from your pet's list", result.SuccessMessage);
        }

        private static DbSet<T> MockDbSet<T>(IEnumerable<T> elements) where T : class
        {
            var queryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSetMock.Object;
        }
    }
}
