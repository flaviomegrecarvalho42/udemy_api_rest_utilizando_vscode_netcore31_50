using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestCreate
{
    public class ReturnBadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é BadRequest.")]
        public async Task When_Post_Method_Return_BadRequest()
        {
            var municipioCreateResultDto = GetMunicipioCreateResultDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Post(It.IsAny<MunicipioCreateDto>()))
                        .ReturnsAsync(municipioCreateResultDto);

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "É um campo obrigatório");

            var municipioCreateDto = GetMunicipioCreateDto();
            var result = await _controller.Post(municipioCreateDto);
            Assert.True(result is BadRequestObjectResult);
        }

        private static MunicipioCreateResultDto GetMunicipioCreateResultDto()
        {
            return new MunicipioCreateResultDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow
            };
        }

        private static MunicipioCreateDto GetMunicipioCreateDto()
        {
            return new MunicipioCreateDto
            {
                Name = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid()
            };
        }
    }
}
