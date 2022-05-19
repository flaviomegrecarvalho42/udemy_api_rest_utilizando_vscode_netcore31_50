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
    public class ReturnOk
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é Ok.")]
        public async Task When_Get_Method_Return_OK()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var userDto = GetUserDto(name, email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(userDto);

            _controller = new UsersController(_serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDto;
            Assert.NotNull(resultValue);
            Assert.Equal(userDto.Id, resultValue.Id);
            Assert.Equal(userDto.Name, resultValue.Name);
            Assert.Equal(userDto.Email, resultValue.Email);
            Assert.Equal(userDto.CreateAt, resultValue.CreateAt);
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
