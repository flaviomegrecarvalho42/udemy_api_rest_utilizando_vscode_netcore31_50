using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class WhenGetMethodIsRun : CepTests
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET.")]
        public async Task Is_Possible_Run_Get_Method()
        {
            #region Teste retornando dados buscando pelo CepId
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Get(CepId))
                        .ReturnsAsync(cepDto);

            _service = _serviceMock.Object;

            var _result = await _service.Get(CepId);
            Assert.NotNull(_result);
            Assert.True(_result.Id == CepId);
            Assert.Equal(Cep, _result.Cep);
            Assert.Equal(Logradouro, _result.Logradouro);
            Assert.Equal(Numero, _result.Numero);
            #endregion

            #region Teste retornando dados buscando pelo Cep
            _serviceMock.Setup(s => s.Get(Cep))
                        .ReturnsAsync(cepDto);

            _service = _serviceMock.Object;

            _result = await _service.Get(Cep);
            Assert.NotNull(_result);
            Assert.True(_result.Id == CepId);
            Assert.Equal(Cep, _result.Cep);
            Assert.Equal(Logradouro, _result.Logradouro);
            Assert.Equal(Numero, _result.Numero);
            #endregion

            #region Teste retornando null
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((CepDto)null));

            _service = _serviceMock.Object;

            _result = await _service.Get(CepId);
            Assert.Null(_result);
            #endregion
        }
    }
}
