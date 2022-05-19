using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            #region User
            CreateMap<UserDto, UserEntity>()
               .ReverseMap();

            CreateMap<UserCreateResultDto, UserEntity>()
               .ReverseMap();

            CreateMap<UserUpdateResultDto, UserEntity>()
               .ReverseMap();
            #endregion

            #region Uf
            CreateMap<UfDto, UfEntity>()
                .ReverseMap();
            #endregion

            #region Municipio
            CreateMap<MunicipioDto, MunicipioEntity>()
               .ReverseMap();

            CreateMap<MunicipioCompletoDto, MunicipioEntity>()
               .ReverseMap();

            CreateMap<MunicipioCreateResultDto, MunicipioEntity>()
               .ReverseMap();

            CreateMap<MunicipioUpdateResultDto, MunicipioEntity>()
               .ReverseMap();
            #endregion

            #region Cep
            CreateMap<CepDto, CepEntity>()
               .ReverseMap();

            CreateMap<CepCreateResultDto, CepEntity>()
               .ReverseMap();

            CreateMap<CepUpdateResultDto, CepEntity>()
               .ReverseMap();
            #endregion
        }
    }
}
