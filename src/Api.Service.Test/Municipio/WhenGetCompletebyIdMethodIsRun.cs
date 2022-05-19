using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class WhenGetCompletebyIdMethodIsRun : MunicipioTests
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET Complete By Id.")]
        public async Task Is_Possible_Run_GetCompleteById_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetCompleteById(MunicipioId))
                        .ReturnsAsync(municipioCompletoDto);

            _service = _serviceMock.Object;

            var _result = await _service.GetCompleteById(MunicipioId);
            Assert.NotNull(_result);
            Assert.True(_result.Id == MunicipioId);
            Assert.Equal(Name, _result.Name);
            Assert.Equal(CodIBGE, _result.CodIBGE);
            Assert.Equal(UfId, _result.UfId);
            Assert.NotNull(_result.Uf);
            #endregion

            #region Teste retornando null
            _serviceMock.Setup(s => s.GetCompleteById(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((MunicipioCompletoDto)null));

            _service = _serviceMock.Object;

            _result = await _service.GetCompleteById(MunicipioId);
            Assert.Null(_result);
            #endregion
        }
    }
}
