using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGetAll
{
    public class ReturnOk
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetAll é Ok.")]
        public async Task When_GetAll_Method_Return_OK()
        {
            var listMunicipioDto = GetListMunicipioDto();

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listMunicipioDto);

            _controller = new MunicipiosController(_serviceMock.Object);

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as List<MunicipioDto>;
            Assert.NotNull(resultValue);

            for (var i = 0; i < resultValue.Count(); i++)
            {
                Assert.Equal(listMunicipioDto.ToList()[i].Id, resultValue.ToList()[i].Id);
                Assert.Equal(listMunicipioDto.ToList()[i].Name, resultValue.ToList()[i].Name);
                Assert.Equal(listMunicipioDto.ToList()[i].CodIBGE, resultValue.ToList()[i].CodIBGE);
                Assert.Equal(listMunicipioDto.ToList()[i].UfId, resultValue.ToList()[i].UfId);
            }
        }

        private static IEnumerable<MunicipioDto> GetListMunicipioDto()
        {
            return new List<MunicipioDto>
            {
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                },
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                }
            };
        }
    }
}
