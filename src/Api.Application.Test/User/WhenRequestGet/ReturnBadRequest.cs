using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestGet
{
    public class ReturnBadRequest
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é BadRequest.")]
        public async Task When_Get_Method_Return_BadRequest()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var userDto = GetUserDto(name, email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(userDto);

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        private static UserDto GetUserDto(string name, string email)
        {
            return new UserDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}
