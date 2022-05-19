using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class WhenCreateMethodIsRun : CepTests
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Create.")]
        public async Task Is_Possible_Run_Create_Method()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(s => s.Post(cepCreateDto))
                        .ReturnsAsync(cepCreateResultDto);

            _service = _serviceMock.Object;

            var _result = await _service.Post(cepCreateDto);
            Assert.NotNull(_result);
            Assert.Equal(Cep, _result.Cep);
            Assert.Equal(Logradouro, _result.Logradouro);
            Assert.Equal(Numero, _result.Numero);
        }
    }
}
