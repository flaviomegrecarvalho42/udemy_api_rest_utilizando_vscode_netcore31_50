using Api.Domain.Entities;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            #region User
            CreateMap<UserModel, UserEntity>()
               .ReverseMap();
            #endregion

            #region Uf
            CreateMap<UfModel, UfEntity>()
               .ReverseMap();
            #endregion

            #region Municipio
            CreateMap<MunicipioModel, MunicipioEntity>()
               .ReverseMap();
            #endregion

            #region Cep
            CreateMap<CepModel, CepEntity>()
               .ReverseMap();
            #endregion
        }
    }
}
