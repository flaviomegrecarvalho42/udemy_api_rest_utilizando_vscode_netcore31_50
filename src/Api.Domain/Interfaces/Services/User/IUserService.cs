using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDto> Get(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserCreateResultDto> Post(UserCreateDto userCreateDto);
        Task<UserUpdateResultDto> Put(UserUpdateDto userUpdateDto);
        Task<bool> Delete(Guid id);
    }
}
