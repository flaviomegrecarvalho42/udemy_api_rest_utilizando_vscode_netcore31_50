using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class WhenCreateMethodIsRun : MunicipioTests
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Create.")]
        public async Task Is_Possible_Run_Create_Method()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(s => s.Post(municipioCreateDto))
                        .ReturnsAsync(municipioCreateResultDto);

            _service = _serviceMock.Object;

            var _result = await _service.Post(municipioCreateDto);
            Assert.NotNull(_result);
            Assert.Equal(Name, _result.Name);
            Assert.Equal(CodIBGE, _result.CodIBGE);
            Assert.Equal(UfId, _result.UfId);
        }
    }
}
