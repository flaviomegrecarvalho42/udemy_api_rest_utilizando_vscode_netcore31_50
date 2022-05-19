using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class WhenUpdateMethodIsRun : UsersTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Update.")]
        public async Task Is_Possible_Run_Update_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Put(userUpdateDto))
            .ReturnsAsync(userUpdateResultDto);

            _service = _serviceMock.Object;

            var _result = await _service.Put(userUpdateDto);
            Assert.NotNull(_result);
            Assert.Equal(UserNameUpdated, _result.Name);
            Assert.Equal(UserEmailUpdated, _result.Email);
        }
    }
}
