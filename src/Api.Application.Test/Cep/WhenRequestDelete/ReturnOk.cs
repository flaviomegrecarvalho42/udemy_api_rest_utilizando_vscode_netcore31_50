using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.WhenRequestDelete
{
    public class ReturnOk
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Delete é Ok.")]
        public async Task When_Delete_Method_Return_Ok()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(true);

            _controller = new CepsController(_serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((Boolean)resultValue);
        }
    }
}
