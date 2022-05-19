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
    public class ReturnCreated
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Post é Created.")]
        public async Task When_Post_Method_Return_Created()
        {
            var municipioCreateResultDto = GetMunicipioCreateResultDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Post(It.IsAny<MunicipioCreateDto>()))
                        .ReturnsAsync(municipioCreateResultDto);

            _controller = new MunicipiosController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");

            _controller.Url = url.Object;

            var municipioCreateDto = GetMunicipioCreateDto();
            var result = await _controller.Post(municipioCreateDto);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as MunicipioCreateResultDto;
            Assert.NotNull(resultValue);
            Assert.Equal(municipioCreateResultDto.Id, resultValue.Id);
            Assert.Equal(municipioCreateResultDto.Name, resultValue.Name);
            Assert.Equal(municipioCreateResultDto.CodIBGE, resultValue.CodIBGE);
            Assert.Equal(municipioCreateResultDto.UfId, resultValue.UfId);
            Assert.Equal(municipioCreateResultDto.CreateAt, resultValue.CreateAt);
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
