using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class WhenGetAllMethodIsRun : MunicipioTests
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GETALL.")]
        public async Task Is_Possible_Run_GetAll_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listMunicipioDto);

            _service = _serviceMock.Object;

            var _result = await _service.GetAll();
            Assert.NotNull(_result);
            Assert.True(_result.Count() == listMunicipioDto.Count());
            #endregion

            #region Teste retornando null
            var _listResult = new List<MunicipioDto>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(_listResult.AsEnumerable);

            _service = _serviceMock.Object;

            _result = await _service.GetAll();
            Assert.Empty(_result);
            Assert.True(_result.Count() == 0);
            #endregion
        }
    }
}
