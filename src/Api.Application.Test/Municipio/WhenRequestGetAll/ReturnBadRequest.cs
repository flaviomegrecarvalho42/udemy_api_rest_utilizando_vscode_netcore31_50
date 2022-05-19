using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGetAll
{
    public class ReturnBadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetAll é BadRequest.")]
        public async Task When_GetAll_Method_Return_BadRequest()
        {
            var listMunicipioDto = GetListMunicipioDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listMunicipioDto);

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.True(!_controller.ModelState.IsValid);
        }

        private static IEnumerable<MunicipioDto> GetListMunicipioDto()
        {
            return new List<MunicipioDto>
            {
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                },
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                }
            };
        }
    }
}
