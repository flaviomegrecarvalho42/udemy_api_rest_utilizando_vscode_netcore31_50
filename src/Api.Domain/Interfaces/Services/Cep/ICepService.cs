using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDto> Get(Guid id);
        Task<CepDto> Get(string cep);
        Task<CepCreateResultDto> Post(CepCreateDto cepCreateDto);
        Task<CepUpdateResultDto> Put(CepUpdateDto cepUpdateDto);
        Task<bool> Delete(Guid id);
    }
}
