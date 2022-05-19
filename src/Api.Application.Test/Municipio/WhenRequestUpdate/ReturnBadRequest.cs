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
    public class ReturnBadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Put é BadRequest.")]
        public async Task When_Put_Method_Return_BadRequest()
        {
            var municipioUpdateResultDto = GetMunicipioUpdateResultDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Put(It.IsAny<MunicipioUpdateDto>()))
                        .ReturnsAsync(municipioUpdateResultDto);

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "É um campo obrigatório");

            var municipioUpdateDto = GetMunicipioUpdateDto();
            var result = await _controller.Put(municipioUpdateDto);
            Assert.True(result is BadRequestObjectResult);
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
