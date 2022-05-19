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
    public class ReturnBadRequest
    {
        private UfsController _controller;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é BadRequest.")]
        public async Task When_Get_Method_Return_BadRequest()
        {
            var ufDto = GetUfDto();

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .ReturnsAsync(ufDto);

            _controller = new UfsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.True(!_controller.ModelState.IsValid);
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
