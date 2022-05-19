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
    public class ReturnBadRequest
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é BadRequest.")]
        public async Task When_Post_Method_Return_BadRequest()
        {
            var cepCreateResultDto = GetCepCreateResultDto();

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Post(It.IsAny<CepCreateDto>()))
                        .ReturnsAsync(cepCreateResultDto);

            _controller = new CepsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Logradouro", "É um campo obrigatório");

            var cepCreateDto = GetCepCreateDto();
            var result = await _controller.Post(cepCreateDto);
            Assert.True(result is BadRequestObjectResult);
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
