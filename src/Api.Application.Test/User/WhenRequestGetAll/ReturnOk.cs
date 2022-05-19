using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestGetAll
{
    public class ReturnOk
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetAll é Ok.")]
        public async Task When_GetAll_Method_Return_OK()
        {
            var listUserDto = GetListUserDto();

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listUserDto);

            _controller = new UsersController(_serviceMock.Object);

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            Assert.True(resultValue.Count() == listUserDto.Count());

            for (var i = 0; i < resultValue.Count(); i++)
            {
                Assert.Equal(listUserDto.ToList()[i].Id, resultValue.ToList()[i].Id);
                Assert.Equal(listUserDto.ToList()[i].Name, resultValue.ToList()[i].Name);
                Assert.Equal(listUserDto.ToList()[i].Email, resultValue.ToList()[i].Email);
                Assert.Equal(listUserDto.ToList()[i].CreateAt, resultValue.ToList()[i].CreateAt);
            }
        }

        private static IEnumerable<UserDto> GetListUserDto()
        {
            var listUserDto = new List<UserDto>();

            for (var i = 0; i < 2; i++)
            {
                var userDto = new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow
                };

                listUserDto.Add(userDto);
            }

            return listUserDto;
        }
    }
}
