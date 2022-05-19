using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.WhenRequestUpdate
{
    public class ReturnOk
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é Created.")]
        public async Task When_Post_Method_Return_Created()
        {
            var cepUpdateResultDto = GetCepUpdateResultDto();

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Put(It.IsAny<CepUpdateDto>()))
                        .ReturnsAsync(cepUpdateResultDto);

            _controller = new CepsController(_serviceMock.Object);

            var cepUpdateDto = GetCepUpdateDto();
            var result = await _controller.Put(cepUpdateDto);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as CepUpdateResultDto;
            Assert.NotNull(resultValue);
            Assert.Equal(cepUpdateResultDto.Id, resultValue.Id);
            Assert.Equal(cepUpdateResultDto.Cep, resultValue.Cep);
            Assert.Equal(cepUpdateResultDto.Logradouro, resultValue.Logradouro);
            Assert.Equal(cepUpdateResultDto.Numero, resultValue.Numero);
            Assert.Equal(cepUpdateResultDto.MunicipioId, resultValue.MunicipioId);
            Assert.Equal(cepUpdateResultDto.UpdateAt, resultValue.UpdateAt);
        }

        private static CepUpdateResultDto GetCepUpdateResultDto()
        {
            return new CepUpdateResultDto
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                MunicipioId = Guid.NewGuid(),
                UpdateAt = DateTime.UtcNow
            };
        }

        private static CepUpdateDto GetCepUpdateDto()
        {
            return new CepUpdateDto
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                MunicipioId = Guid.NewGuid()
            };
        }

    }
}
