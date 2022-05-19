using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class WhenCreateMethodIsRun : UsersTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Create.")]
        public async Task Is_Possible_Run_Create_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Post(userCreateDto))
                        .ReturnsAsync(userCreateResultDto);

            _service = _serviceMock.Object;

            var _result = await _service.Post(userCreateDto);
            Assert.NotNull(_result);
            Assert.Equal(UserName, _result.Name);
            Assert.Equal(UserEmail, _result.Email);
        }
    }
}
