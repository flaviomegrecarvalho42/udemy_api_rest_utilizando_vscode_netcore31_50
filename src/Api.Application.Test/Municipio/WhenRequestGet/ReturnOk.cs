using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGet
{
    public class ReturnOk
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é Ok.")]
        public async Task When_Get_Method_Return_OK()
        {
            var municipioDto = GetMunicipioDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(municipioDto);

            _controller = new MunicipiosController(_serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as MunicipioDto;
            Assert.NotNull(resultValue);
            Assert.Equal(municipioDto.Id, resultValue.Id);
            Assert.Equal(municipioDto.Name, resultValue.Name);
            Assert.Equal(municipioDto.UfId, resultValue.UfId);
        }

        private static MunicipioDto GetMunicipioDto()
        {
            return new MunicipioDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid()
            };
        }
    }
}
