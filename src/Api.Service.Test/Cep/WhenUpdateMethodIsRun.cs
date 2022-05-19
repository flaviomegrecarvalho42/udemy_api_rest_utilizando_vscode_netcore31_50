using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class WhenUpdateMethodIsRun : CepTests
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Update.")]
        public async Task Is_Possible_Run_Update_Method()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Put(cepUpdateDto))
            .ReturnsAsync(cepUpdateResultDto);

            _service = _serviceMock.Object;

            var _result = await _service.Put(cepUpdateDto);
            Assert.NotNull(_result);
            Assert.Equal(CepUpdated, _result.Cep);
            Assert.Equal(LogradouroUpdated, _result.Logradouro);
            Assert.Equal(NumeroUpdated, _result.Numero);
        }
    }
}
