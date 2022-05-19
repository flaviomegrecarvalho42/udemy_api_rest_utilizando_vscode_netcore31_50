using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGetCompletebyIBGE
{
    public class ReturnGet
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetCompleteByIBGE é NotFound.")]
        public async Task When_GetCompleteByIBGE_Method_Return_Not_Found()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetCompleteByIBGE(It.IsAny<int>()))
                        .Returns(Task.FromResult((MunicipioCompletoDto)null));

            _controller = new MunicipiosController(_serviceMock.Object);

            var result = await _controller.GetCompleteByIBGE(It.IsAny<int>());
            Assert.True(result is NotFoundResult);
        }
    }
}
