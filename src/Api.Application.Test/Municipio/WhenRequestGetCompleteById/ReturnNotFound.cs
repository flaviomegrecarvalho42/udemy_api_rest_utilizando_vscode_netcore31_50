using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.WhenRequestGetCompleteById
{
    public class ReturnGet
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetCompleteById é NotFound.")]
        public async Task When_GetCompleteById_Method_Return_Not_Found()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetCompleteById(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((MunicipioCompletoDto)null));

            _controller = new MunicipiosController(_serviceMock.Object);

            var result = await _controller.GetCompleteById(default(Guid));
            Assert.True(result is NotFoundResult);
        }
    }
}
