using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BikeShop.Application.Commands;
using BikeShop.Application.Exceptions;
using BikeShop.Application.Handlers;
using BikeShop.Domain.Entities;
using BikeShop.Domain.Interfaces;
using Moq;
using Xunit;

namespace BikeShop.Api.Tests.Handlers
{
    public class CreateBikeHandlerTests
    {
        private readonly Mock<IBikeRepository> _repo;
        private readonly CreateBikeHandler _handler;
        private Bike _capturedBike = null!;

        public CreateBikeHandlerTests()
        {
            _repo = new Mock<IBikeRepository>();
            _handler = new CreateBikeHandler(_repo.Object);
        }

        [Fact]
        public async Task Handle_NewBike_CallsAddAndSave_AndReturnsAssignedId()
        {
            // Arrange: no existing bike
            _repo
                .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Bike, bool>>>()))
                .ReturnsAsync(false);

            // Capture the Bike passed into AddAsync
            _repo
                .Setup(r => r.AddAsync(It.IsAny<Bike>()))
                .Callback<Bike>(b => _capturedBike = b)
                .Returns(Task.CompletedTask);

            // When SaveChangesAsync runs, use reflection to set the (non-public) Id
            _repo
                .Setup(r => r.SaveChangesAsync())
                .Callback(() => {
                    var idProp = typeof(Bike)
                        .GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    idProp!.SetValue(_capturedBike, 123);
                })
                .ReturnsAsync(1);

            var cmd = new CreateBikeCommand(
                manufacturer: "Mfg",
                model: "Model",
                price: 100,
                category: "Cat",
                colour: "Clr",
                weight: "1kg",
                imgUrl: "url.png"
            );

            // Act
            var resultId = await _handler.Handle(cmd, CancellationToken.None);

            // Assert that we invoked the right repo methods
            _repo.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<Bike, bool>>>()), Times.Once);
            _repo.Verify(r => r.AddAsync(It.IsAny<Bike>()), Times.Once);
            _repo.Verify(r => r.SaveChangesAsync(), Times.Once);

            // And that the handler returns the reflected Id
            Assert.Equal(123, resultId);
        }

        [Fact]
        public async Task Handle_DuplicateBike_ThrowsDuplicateBikeException()
        {
            // Arrange: repository reports that the bike already exists
            _repo
                .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Bike, bool>>>()))
                .ReturnsAsync(true);

            var cmd = new CreateBikeCommand(
                manufacturer: "Same",
                model: "Same",
                price: 100,
                category: "Cat",
                colour: "Clr",
                weight: "1kg",
                imgUrl: "url.png"
            );

            // Act & Assert
            await Assert.ThrowsAsync<DuplicateBikeException>(() =>
                _handler.Handle(cmd, CancellationToken.None)
            );

            // And make sure we never added or saved
            _repo.Verify(r => r.AddAsync(It.IsAny<Bike>()), Times.Never);
            _repo.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
