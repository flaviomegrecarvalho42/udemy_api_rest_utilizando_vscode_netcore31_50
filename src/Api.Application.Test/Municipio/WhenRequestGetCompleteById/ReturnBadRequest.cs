using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGetCompleteById
{
    public class ReturnBadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetCompleteById é BadRequest.")]
        public async Task When_Method_GetCompleteById_Return_BadRequest()
        {
            var municipioCompleteDto = GetMunicipioCompleteDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetCompleteById(It.IsAny<Guid>()))
                        .ReturnsAsync(municipioCompleteDto);

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.GetCompleteById(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
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
