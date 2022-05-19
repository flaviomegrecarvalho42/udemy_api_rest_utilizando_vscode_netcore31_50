using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestDelete
{
    public class ReturnBadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Delete é BadRequest.")]
        public async Task When_Delete_Method_Return_BadRequest()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(true);

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
