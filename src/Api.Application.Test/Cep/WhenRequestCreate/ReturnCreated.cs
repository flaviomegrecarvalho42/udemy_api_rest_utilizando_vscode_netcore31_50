using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.WhenRequestCreate
{
    public class ReturnCreated
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é Created.")]
        public async Task When_Post_Method_Return_Created()
        {
            var cepCreateResultDto = GetCepCreateResultDto();

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Post(It.IsAny<CepCreateDto>()))
                        .ReturnsAsync(cepCreateResultDto);

            _controller = new CepsController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");

            _controller.Url = url.Object;

            var cepCreateDto = GetCepCreateDto();
            var result = await _controller.Post(cepCreateDto);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as CepCreateResultDto;
            Assert.NotNull(resultValue);
            Assert.Equal(cepCreateResultDto.Id, resultValue.Id);
            Assert.Equal(cepCreateResultDto.Cep, resultValue.Cep);
            Assert.Equal(cepCreateResultDto.Logradouro, resultValue.Logradouro);
            Assert.Equal(cepCreateResultDto.Numero, resultValue.Numero);
            Assert.Equal(cepCreateResultDto.MunicipioId, resultValue.MunicipioId);
            Assert.Equal(cepCreateResultDto.CreateAt, resultValue.CreateAt);
        }

        private static CepCreateResultDto GetCepCreateResultDto()
        {
            return new CepCreateResultDto
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                MunicipioId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow
            };
        }

        private static CepCreateDto GetCepCreateDto()
        {
            return new CepCreateDto
            {
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                MunicipioId = Guid.NewGuid()
            };
        }
    }
}
