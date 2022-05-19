using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.WhenRequestGet
{
    public class ReturnOk
    {
        private UfsController _controller;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é Ok.")]
        public async Task When_Get_Method_Return_OK()
        {
            var ufDto = GetUfDto();

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(ufDto);

            _controller = new UfsController(_serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UfDto;
            Assert.NotNull(resultValue);
            Assert.Equal(ufDto.Id, resultValue.Id);
            Assert.Equal(ufDto.Name, resultValue.Name);
            Assert.Equal(ufDto.Sigla, resultValue.Sigla);
        }

        private static UfDto GetUfDto()
        {
            return new UfDto
            {
                Id = Guid.NewGuid(),
                Name = "Rio de Janeiro",
                Sigla = "RJ"
            };
        }
    }
}
