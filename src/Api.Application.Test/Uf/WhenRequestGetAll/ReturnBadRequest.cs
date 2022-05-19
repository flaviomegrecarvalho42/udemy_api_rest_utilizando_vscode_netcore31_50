using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.WhenRequestGetAll
{
    public class ReturnBadRequest
    {
        private UfsController _controller;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "Quando o retorno do método GetAll é BadRequest.")]
        public async Task When_GetAll_Method_Return_BadRequest()
        {
            var listUfDto = GetListUfDto();

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listUfDto);

            _controller = new UfsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.True(!_controller.ModelState.IsValid);
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
