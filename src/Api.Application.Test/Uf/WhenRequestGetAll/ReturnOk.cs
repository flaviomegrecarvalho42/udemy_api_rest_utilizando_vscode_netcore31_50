using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.WhenRequestGetAll
{
    public class ReturnOk
    {
        private UfsController _controller;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetAll é Ok.")]
        public async Task When_GetAll_Method_Return_OK()
        {
            var listUfDto = GetListUfDto();

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listUfDto);

            _controller = new UfsController(_serviceMock.Object);

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as List<UfDto>;
            Assert.NotNull(resultValue);

            for (var i = 0; i < resultValue.Count(); i++)
            {
                Assert.Equal(listUfDto.ToList()[i].Id, resultValue.ToList()[i].Id);
                Assert.Equal(listUfDto.ToList()[i].Name, resultValue.ToList()[i].Name);
                Assert.Equal(listUfDto.ToList()[i].Sigla, resultValue.ToList()[i].Sigla);
            }
        }

        private static IEnumerable<UfDto> GetListUfDto()
        {
            return new List<UfDto>
            {
                new UfDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Rio de Janeiro",
                    Sigla = "RJ"
                },
                new UfDto
                {
                    Id = Guid.NewGuid(),
                    Name = "São Paulo",
                    Sigla = "SP"
                }
            };
        }
    }
}
