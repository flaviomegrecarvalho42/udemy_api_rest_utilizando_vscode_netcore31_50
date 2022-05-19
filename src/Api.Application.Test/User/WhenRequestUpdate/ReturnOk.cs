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
    public class ReturnOk
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Put é Ok.")]
        public async Task When_Put_Method_Return_Ok()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var userUpdateResultDto = GetUserUpdateResultDto(name, email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Put(It.IsAny<UserUpdateDto>()))
                        .ReturnsAsync(userUpdateResultDto);

            _controller = new UsersController(_serviceMock.Object);

            var userUpdateDto = GetUserUpdateDto(name, email);
            var result = await _controller.Put(userUpdateDto);
            Assert.True(result is OkObjectResult);
            Assert.True(_controller.ModelState.IsValid);

            var resultValue = ((OkObjectResult)result).Value as UserUpdateResultDto;
            Assert.NotNull(resultValue);
            Assert.Equal(userUpdateResultDto.Id, resultValue.Id);
            Assert.Equal(userUpdateResultDto.Name, resultValue.Name);
            Assert.Equal(userUpdateResultDto.Email, resultValue.Email);
            Assert.Equal(userUpdateResultDto.UpdateAt, resultValue.UpdateAt);
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
