using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possível mapear os modelos de Municipio")]
        public void Is_Possible_Map_Models_Municipio()
        {
            var model = GetMunicipioModel();
            var listEntity = GetListMunicipioEntity();

            #region  Model => Entity (ModelToEntityProfile)
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.CodIBGE, entity.CodIBGE);
            Assert.Equal(model.UfId, entity.UfId);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);
            #endregion

            #region Entity => Dto (EntityToDtoProfile)
            var municipioDto = Mapper.Map<MunicipioDto>(entity);
            Assert.Equal(entity.Id, municipioDto.Id);
            Assert.Equal(entity.Name, municipioDto.Name);
            Assert.Equal(entity.CodIBGE, municipioDto.CodIBGE);
            Assert.Equal(entity.UfId, municipioDto.UfId);

            var municipioCompletoDto = Mapper.Map<MunicipioCompletoDto>(listEntity.FirstOrDefault());
            Assert.Equal(listEntity.FirstOrDefault().Id, municipioCompletoDto.Id);
            Assert.Equal(listEntity.FirstOrDefault().Name, municipioCompletoDto.Name);
            Assert.Equal(listEntity.FirstOrDefault().CodIBGE, municipioCompletoDto.CodIBGE);
            Assert.Equal(listEntity.FirstOrDefault().UfId, municipioCompletoDto.UfId);
            Assert.NotNull(municipioCompletoDto.Uf);

            var listDto = Mapper.Map<List<MunicipioDto>>(listEntity);
            Assert.True(listEntity.Count() == listDto.Count());
            for (var i = 0; i < listDto.Count(); i++)
            {
                Assert.Equal(listEntity[i].Id, listDto[i].Id);
                Assert.Equal(listEntity[i].CodIBGE, listDto[i].CodIBGE);
                Assert.Equal(listEntity[i].Name, listDto[i].Name);
                Assert.Equal(listEntity[i].UfId, listDto[i].UfId);
            }

            var municipioCreateResultDto = Mapper.Map<MunicipioCreateResultDto>(entity);
            Assert.Equal(entity.Name, municipioCreateResultDto.Name);
            Assert.Equal(entity.CodIBGE, municipioCreateResultDto.CodIBGE);
            Assert.Equal(entity.UfId, municipioCreateResultDto.UfId);
            Assert.Equal(entity.CreateAt, municipioCreateResultDto.CreateAt);

            var municipioUpdateResultDto = Mapper.Map<MunicipioUpdateResultDto>(entity);
            Assert.Equal(entity.Id, municipioUpdateResultDto.Id);
            Assert.Equal(entity.Name, municipioUpdateResultDto.Name);
            Assert.Equal(entity.CodIBGE, municipioUpdateResultDto.CodIBGE);
            Assert.Equal(entity.UfId, municipioUpdateResultDto.UfId);
            Assert.Equal(entity.UpdateAt, municipioUpdateResultDto.UpdateAt);
            #endregion

            #region Dto => Model (DtoToModelProfile)
            var municipioModel = Mapper.Map<MunicipioModel>(municipioDto);
            Assert.Equal(municipioDto.Id, municipioModel.Id);
            Assert.Equal(municipioDto.Name, municipioModel.Name);
            Assert.Equal(municipioDto.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(municipioDto.UfId, municipioModel.UfId);

            var municipioCreateDto = Mapper.Map<MunicipioCreateDto>(municipioModel);
            Assert.Equal(municipioModel.Name, municipioCreateDto.Name);
            Assert.Equal(municipioModel.CodIBGE, municipioCreateDto.CodIBGE);
            Assert.Equal(municipioModel.UfId, municipioCreateDto.UfId);

            var municipioUpdateDto = Mapper.Map<MunicipioUpdateDto>(municipioModel);
            Assert.Equal(municipioModel.Id, municipioUpdateDto.Id);
            Assert.Equal(municipioModel.Name, municipioUpdateDto.Name);
            Assert.Equal(municipioModel.CodIBGE, municipioUpdateDto.CodIBGE);
            Assert.Equal(municipioModel.UfId, municipioUpdateDto.UfId);
            #endregion
        }

        private static MunicipioModel GetMunicipioModel()
        {
            return new MunicipioModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }

        private static List<MunicipioEntity> GetListMunicipioEntity()
        {
            var listEntity = new List<MunicipioEntity>();

            for (var i = 0; i < 5; i++)
            {
                var item = new MunicipioEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                };

                listEntity.Add(item);
            }

            return listEntity;
        }
    }
}
