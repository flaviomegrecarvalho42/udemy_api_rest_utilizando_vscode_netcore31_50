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
    public class ReturnBadRequest
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Put é BadRequest.")]
        public async Task When_Put_Method_Return_BadRequest()
        {
            var cepCreateResultDto = GetCepUpdateResultDto();

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Put(It.IsAny<CepUpdateDto>()))
                        .ReturnsAsync(cepCreateResultDto);

            _controller = new CepsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Logradouro", "É um campo obrigatório");

            var CepUpdateDto = GetCepUpdateDto();
            var result = await _controller.Put(CepUpdateDto);
            Assert.True(result is BadRequestObjectResult);
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
