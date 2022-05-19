using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class WhenGetAllMethodIsRun : UfTests
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GETALL.")]
        public async Task Is_Possible_Run_GetAll_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listUfDto);

            _service = _serviceMock.Object;

            var _result = await _service.GetAll();
            Assert.NotNull(_result);
            Assert.True(_result.Count() == listUfDto.Count());
            #endregion

            #region Teste retornando null
            var _listResult = new List<UfDto>();
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
