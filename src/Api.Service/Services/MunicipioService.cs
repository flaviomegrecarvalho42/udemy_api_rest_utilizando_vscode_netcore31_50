using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class MunicipioService : IMunicipioService
    {
        private IMunicipioRepository _repository;
        private readonly IMapper _mapper;

        public MunicipioService(IMunicipioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MunicipioDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<MunicipioDto>(entity);
        }

        public async Task<MunicipioCompletoDto> GetCompleteById(Guid id)
        {
            var entity = await _repository.GetCompleteById(id);

            return _mapper.Map<MunicipioCompletoDto>(entity);
        }

        public async Task<MunicipioCompletoDto> GetCompleteByIBGE(int codIBGE)
        {
            var entity = await _repository.GetCompleteByIBGE(codIBGE);

            return _mapper.Map<MunicipioCompletoDto>(entity);
        }

        public async Task<IEnumerable<MunicipioDto>> GetAll()
        {
            var listEntity = await _repository.SelectAsync();

            return _mapper.Map<IEnumerable<MunicipioDto>>(listEntity);
        }

        public async Task<MunicipioCreateResultDto> Post(MunicipioCreateDto municipioCreateDto)
        {
            var model = _mapper.Map<MunicipioModel>(municipioCreateDto);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<MunicipioCreateResultDto>(result);
        }

        public async Task<MunicipioUpdateResultDto> Put(MunicipioUpdateDto municipioUpdateDto)
        {
            var model = _mapper.Map<MunicipioModel>(municipioUpdateDto);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<MunicipioUpdateResultDto>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
