using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class WhenGetAllMethodIsRun : UsersTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GETALL.")]
        public async Task Is_Possible_Run_GetAll_Method()
        {
            #region Teste retornando dados
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(s => s.GetAll())
                        .ReturnsAsync(listUserDto);

            _service = _serviceMock.Object;

            var _result = await _service.GetAll();
            Assert.NotNull(_result);
            Assert.True(_result.Count() == listUserDto.Count());
            #endregion

            #region Teste retornando null
            var _listResult = new List<UserDto>();
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
