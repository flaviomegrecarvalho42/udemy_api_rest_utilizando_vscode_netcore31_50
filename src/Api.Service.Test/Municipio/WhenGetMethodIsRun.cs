using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class WhenGetMethodIsRun : MunicipioTests
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET.")]
        public async Task Is_Possible_Run_Get_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Get(MunicipioId))
                        .ReturnsAsync(municipioDto);

            _service = _serviceMock.Object;

            var _result = await _service.Get(MunicipioId);
            Assert.NotNull(_result);
            Assert.True(_result.Id == MunicipioId);
            Assert.Equal(Name, _result.Name);
            Assert.Equal(CodIBGE, _result.CodIBGE);
            Assert.Equal(UfId, _result.UfId);
            #endregion

            #region Teste retornando null
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((MunicipioDto)null));

            _service = _serviceMock.Object;

            _result = await _service.Get(MunicipioId);
            Assert.Null(_result);
            #endregion
        }
    }
}
