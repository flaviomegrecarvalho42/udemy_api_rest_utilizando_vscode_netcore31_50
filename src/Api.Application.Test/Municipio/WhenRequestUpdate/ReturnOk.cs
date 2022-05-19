using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestUpdate
{
    public class ReturnOk
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Put é Ok.")]
        public async Task When_Put_Method_Return_Ok()
        {
            var municipioUpdateResultDto = GetMunicipioUpdateResultDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Put(It.IsAny<MunicipioUpdateDto>()))
                        .ReturnsAsync(municipioUpdateResultDto);

            _controller = new MunicipiosController(_serviceMock.Object);

            var municipioUpdateDto = GetMunicipioUpdateDto();
            var result = await _controller.Put(municipioUpdateDto);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as MunicipioUpdateResultDto;
            Assert.NotNull(resultValue);
            Assert.Equal(municipioUpdateResultDto.Id, resultValue.Id);
            Assert.Equal(municipioUpdateResultDto.Name, resultValue.Name);
            Assert.Equal(municipioUpdateResultDto.CodIBGE, resultValue.CodIBGE);
            Assert.Equal(municipioUpdateResultDto.UfId, resultValue.UfId);
            Assert.Equal(municipioUpdateResultDto.UpdateAt, resultValue.UpdateAt);
        }

        private static MunicipioUpdateResultDto GetMunicipioUpdateResultDto()
        {
            return new MunicipioUpdateResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid(),
                UpdateAt = DateTime.UtcNow
            };
        }

        private static MunicipioUpdateDto GetMunicipioUpdateDto()
        {
            return new MunicipioUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid()
            };
        }
    }
}
