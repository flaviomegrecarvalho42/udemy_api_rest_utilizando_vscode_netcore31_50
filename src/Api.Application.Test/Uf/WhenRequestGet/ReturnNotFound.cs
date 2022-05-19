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
    public class ReturnNotFound
    {
        private UfsController _controller;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método Get é NotFound.")]
        public async Task When_Get_Method_Return_Not_Found()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((UfDto)null));

            _controller = new UfsController(_serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}
