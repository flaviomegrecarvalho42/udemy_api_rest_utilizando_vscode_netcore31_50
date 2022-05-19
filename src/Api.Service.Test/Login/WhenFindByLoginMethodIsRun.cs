using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class WhenFindByLoginMethodIsRun
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método FindByLogin.")]
        public async Task Is_Possible_Run_Method_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var retunrObject = GetObjectMock(email);
            LoginDto loginDto = GetLoginDto(email);

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(s => s.FindByLogin(loginDto))
                        .ReturnsAsync(retunrObject);

            _service = _serviceMock.Object;

            var _result = await _service.FindByLogin(loginDto);
            Assert.NotNull(_result);
        }

        private static object GetObjectMock(string email)
        {
            return new
            {
                authenticated = true,
                create = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                accessToken = Guid.NewGuid(),
                userName = email,
                name = Faker.Name.FullName(),
                message = "Usuário Logado com sucesso"
            };
        }

        private static LoginDto GetLoginDto(string email)
        {
            return new LoginDto
            {
                Email = email
            };
        }
    }
}
