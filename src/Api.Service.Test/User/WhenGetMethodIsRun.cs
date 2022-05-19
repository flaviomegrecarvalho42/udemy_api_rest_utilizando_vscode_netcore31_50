using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class WhenGetMethodIsRun : UsersTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET.")]
        public async Task Is_Possible_Run_Get_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.Get(UserId))
                        .ReturnsAsync(userDto);

            _service = _serviceMock.Object;

            var _result = await _service.Get(UserId);
            Assert.NotNull(_result);
            Assert.True(_result.Id == UserId);
            Assert.Equal(UserName, _result.Name);
            #endregion

            #region Teste retornando null
            _serviceMock.Setup(s => s.Get(It.IsAny<Guid>()))
                        .Returns(Task.FromResult((UserDto)null));

            _service = _serviceMock.Object;

            _result = await _service.Get(UserId);
            Assert.Null(_result);
            #endregion
        }
    }
}
