using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;

namespace Api.Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {
        Task<MunicipioDto> Get(Guid id);
        Task<MunicipioCompletoDto> GetCompleteById(Guid id);
        Task<MunicipioCompletoDto> GetCompleteByIBGE(int codIBGE);
        Task<IEnumerable<MunicipioDto>> GetAll();
        Task<MunicipioCreateResultDto> Post(MunicipioCreateDto municipioCreateDto);
        Task<MunicipioUpdateResultDto> Put(MunicipioUpdateDto municipioUpdateDto);
        Task<bool> Delete(Guid id);
    }
}
