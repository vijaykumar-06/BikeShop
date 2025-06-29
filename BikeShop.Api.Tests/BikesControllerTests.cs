using System.Collections.Generic;
using System.Threading.Tasks;
using BikeShop.Api.Controllers;
using BikeShop.Application.Commands;
using BikeShop.Application.DTOs;
using BikeShop.Application.Exceptions;
using BikeShop.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BikeShop.Api.Tests.Controllers
{
    public class BikesControllerTests
    {
        private readonly Mock<IMediator> _mediator = new();
        private readonly BikesController _ctrl;

        public BikesControllerTests()
        {
            _ctrl = new BikesController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithList()
        {
            // Arrange
            var list = new List<BikeDto> { new() { Id = 1, Manufacturer = "T", Model = "M" } };
            _mediator.Setup(m => m.Send(It.IsAny<GetAllBikesQuery>(), default))
                     .ReturnsAsync(list);

            // Act
            var result = await _ctrl.GetAll();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(list, ok.Value);
        }

        [Fact]
        public async Task Create_OnSuccess_ReturnsCreatedAtAction()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<CreateBikeCommand>(), default))
                     .ReturnsAsync(42);

            var dto = new BikeDto { Manufacturer = "T", Model = "M", Price = 50 };

            // Act
            var result = await _ctrl.Create(dto);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", created.ActionName);
            Assert.Equal(42, created.Value);
        }

        [Fact]
        public async Task Create_Duplicate_ReturnsConflict()
        {
            // Arrange
            _mediator.Setup(m => m.Send(It.IsAny<CreateBikeCommand>(), default))
                     .ThrowsAsync(new DuplicateBikeException("dup"));

            // Act
            var result = await _ctrl.Create(new BikeDto());

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}
