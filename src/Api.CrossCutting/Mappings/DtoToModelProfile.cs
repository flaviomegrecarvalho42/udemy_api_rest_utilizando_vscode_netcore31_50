using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region User
            CreateMap<UserModel, UserDto>()
                .ReverseMap();

            CreateMap<UserModel, UserCreateDto>()
                .ReverseMap();

            CreateMap<UserModel, UserUpdateDto>()
                .ReverseMap();
            #endregion

            #region Uf
            CreateMap<UfModel, UfDto>()
                .ReverseMap();
            #endregion

            #region Municipio
            CreateMap<MunicipioModel, MunicipioDto>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioCreateDto>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioUpdateDto>()
                .ReverseMap();
            #endregion

            #region Cep
            CreateMap<CepModel, CepDto>()
                            .ReverseMap();

            CreateMap<CepModel, CepCreateDto>()
                .ReverseMap();

            CreateMap<CepModel, CepUpdateDto>()
                .ReverseMap();
            #endregion
        }
    }
}
