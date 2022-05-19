using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGetCompletebyIBGE
{
    public class ReturnOk
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetCompleteByIBGE é Ok.")]
        public async Task When_GetCompleteByIBGE_Method_Return_OK()
        {
            var municipioCompleteDto = GetMunicipioCompleteDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetCompleteByIBGE(It.IsAny<int>()))
                        .ReturnsAsync(municipioCompleteDto);

            _controller = new MunicipiosController(_serviceMock.Object);

            var result = await _controller.GetCompleteByIBGE(It.IsAny<int>());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as MunicipioCompletoDto;
            Assert.NotNull(resultValue);
            Assert.Equal(municipioCompleteDto.Id, resultValue.Id);
            Assert.Equal(municipioCompleteDto.Name, resultValue.Name);
            Assert.Equal(municipioCompleteDto.UfId, resultValue.UfId);
            Assert.NotNull(resultValue.Uf);
        }

        private static MunicipioCompletoDto GetMunicipioCompleteDto()
        {
            return new MunicipioCompletoDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid(),
                Uf = new UfDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                }
            };
        }
    }
}
