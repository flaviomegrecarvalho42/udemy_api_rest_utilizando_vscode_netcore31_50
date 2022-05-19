using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.WhenRequestGet
{
    public class ReturnBadRequest
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é BadRequest.")]
        public async Task When_Get_Method_Return_BadRequest()
        {
            var cepDto = GetCepDto();

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(cepDto);

            _controller = new CepsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        private static CepDto GetCepDto()
        {
            return new CepDto
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                MunicipioId = Guid.NewGuid(),
                Municipio = new MunicipioCompletoDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                }
            };
        }
    }
}
