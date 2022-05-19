using System.Threading.Tasks;
using Api.Domain.Dtos.Login;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto loginDto);
    }
}
