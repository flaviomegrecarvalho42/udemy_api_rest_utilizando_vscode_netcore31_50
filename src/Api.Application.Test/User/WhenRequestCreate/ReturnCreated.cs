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
    public class ReturnCreated
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é Created.")]
        public async Task When_Post_Method_Return_Created()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var userCreateResultDto = GetUserCreateResultDto(name, email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Post(It.IsAny<UserCreateDto>()))
                        .ReturnsAsync(userCreateResultDto);

            _controller = new UsersController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");

            _controller.Url = url.Object;

            var userCreateDto = GetUserCreateDto(name, email);
            var result = await _controller.Post(userCreateDto);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as UserCreateResultDto;
            Assert.NotNull(resultValue);
            Assert.Equal(userCreateResultDto.Id, resultValue.Id);
            Assert.Equal(userCreateResultDto.Name, resultValue.Name);
            Assert.Equal(userCreateResultDto.Email, resultValue.Email);
            Assert.Equal(userCreateResultDto.CreateAt, resultValue.CreateAt);
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
