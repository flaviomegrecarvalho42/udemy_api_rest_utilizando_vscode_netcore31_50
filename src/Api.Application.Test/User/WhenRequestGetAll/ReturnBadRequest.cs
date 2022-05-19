using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestGetAll
{
    public class ReturnBadRequest
    {
        private UsersController _controller;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetAll é BadRequest.")]
        public async Task When_GetAll_Method_Return_BadRequest()
        {
            var listUserDto = GetListUserDto();

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listUserDto);

            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
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
