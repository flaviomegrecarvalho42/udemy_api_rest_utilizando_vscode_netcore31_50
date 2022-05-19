using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestUpdate
{
    public class ReturnBadRequest
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Put é BadRequest.")]
        public async Task When_Put_Method_Return_BadRequest()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var userUpdateResultDto = GetUserUpdateResultDto(name, email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Put(It.IsAny<UserUpdateDto>()))
                        .ReturnsAsync(userUpdateResultDto);

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Email", "É um campo obrigatório");

            var userUpdateDto = GetUserUpdateDto(name, email);
            var result = await _controller.Put(userUpdateDto);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        private static UserUpdateResultDto GetUserUpdateResultDto(string name, string email)
        {
            return new UserUpdateResultDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                UpdateAt = DateTime.UtcNow
            };
        }

        private static UserUpdateDto GetUserUpdateDto(string name, string email)
        {
            return new UserUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };
        }
    }
}
