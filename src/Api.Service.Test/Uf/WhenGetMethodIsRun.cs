using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class WhenGetMethodIsRun : UfTests
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET.")]
        public async Task Is_Possible_Run_Get_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.Get(IdUf))
                        .ReturnsAsync(ufDto);

            _service = _serviceMock.Object;

            var _result = await _service.Get(IdUf);
            Assert.NotNull(_result);
            Assert.True(_result.Id == IdUf);
            Assert.Equal(Name, _result.Name);
            #endregion

            #region Teste retornando null
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((UfDto)null));

            _service = _serviceMock.Object;

            _result = await _service.Get(IdUf);
            Assert.Null(_result);
            #endregion
        }
    }
}
