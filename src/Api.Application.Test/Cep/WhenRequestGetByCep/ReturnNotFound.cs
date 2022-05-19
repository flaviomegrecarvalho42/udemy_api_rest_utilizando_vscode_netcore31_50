using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.WhenRequestGetByCep
{
    public class ReturnNotFound
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetByCep é NotFound.")]
        public async Task When_GetByCep_Method_Return_Not_Found()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<string>()))
                        .Returns(Task.FromResult((CepDto)null));

            _controller = new CepsController(_serviceMock.Object);

            var result = await _controller.Get(It.IsAny<string>());
            Assert.True(result is NotFoundResult);
        }
    }
}
