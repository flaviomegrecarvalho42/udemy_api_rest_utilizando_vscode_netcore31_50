using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class WhenUpdateMethodIsRun : MunicipioTests
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Update.")]
        public async Task Is_Possible_Run_Update_Method()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Put(municipioUpdateDto))
            .ReturnsAsync(municipioUpdateResultDto);

            _service = _serviceMock.Object;

            var _result = await _service.Put(municipioUpdateDto);
            Assert.NotNull(_result);
            Assert.Equal(NameUpdated, _result.Name);
            Assert.Equal(CodIBGEUpdated, _result.CodIBGE);
            Assert.Equal(UfId, _result.UfId);
        }
    }
}
