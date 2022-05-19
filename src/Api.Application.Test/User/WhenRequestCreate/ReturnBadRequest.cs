using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestCreate
{
    public class ReturnBadRequest
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é BadRequest.")]
        public async Task When_Post_Method_Return_BadRequest()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var userCreateResultDto = GetUserCreateResultDto(name, email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Post(It.IsAny<UserCreateDto>()))
                        .ReturnsAsync(userCreateResultDto);

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "É um campo obrigatório");

            var userCreateDto = GetUserCreateDto(name, email);
            var result = await _controller.Post(userCreateDto);
            Assert.True(result is BadRequestObjectResult);
        }

        private static UserCreateResultDto GetUserCreateResultDto(string name, string email)
        {
            return new UserCreateResultDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                CreateAt = DateTime.UtcNow
            };
        }

        private static UserCreateDto GetUserCreateDto(string name, string email)
        {
            return new UserCreateDto
            {
                Name = name,
                Email = email
            };
        }
    }
}
