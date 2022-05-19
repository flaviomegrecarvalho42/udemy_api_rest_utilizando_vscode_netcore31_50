using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestDelete
{
    public class ReturnOk
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Delete é Ok.")]
        public async Task When_Delete_Method_Return_Ok()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(true);

            _controller = new MunicipiosController(_serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((Boolean)resultValue);
        }
    }
}
