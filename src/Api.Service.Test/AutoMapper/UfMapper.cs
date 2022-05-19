using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Uf;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTestService
    {
        [Fact(DisplayName = "É possível mapear os modelos de UF")]
        public void Is_Possible_Map_Models_UF()
        {
            var model = GetUfModel();
            var listEntity = GetLisUfEntity();

            #region  Model => Entity (ModelToEntityProfile)
            var entity = Mapper.Map<UfEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.Sigla, entity.Sigla);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);
            #endregion

            #region Entity => Dto (EntityToDtoProfile)
            var ufDto = Mapper.Map<UfDto>(entity);
            Assert.Equal(entity.Id, ufDto.Id);
            Assert.Equal(entity.Name, ufDto.Name);
            Assert.Equal(entity.Sigla, ufDto.Sigla);

            var listDto = Mapper.Map<List<UfDto>>(listEntity);
            Assert.True(listEntity.Count() == listDto.Count());
            for (var i = 0; i < listDto.Count(); i++)
            {
                Assert.Equal(listEntity[i].Id, listDto[i].Id);
                Assert.Equal(listEntity[i].Sigla, listDto[i].Sigla);
                Assert.Equal(listEntity[i].Name, listDto[i].Name);
            }
            #endregion

            #region Dto => Model (DtoToModelProfile)
            var ufModel = Mapper.Map<UfModel>(ufDto);
            Assert.Equal(ufDto.Id, ufModel.Id);
            Assert.Equal(ufDto.Name, ufModel.Name);
            Assert.Equal(ufDto.Sigla, ufModel.Sigla);
            #endregion
        }

        private static UfModel GetUfModel()
        {
            return new UfModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }

        private static List<UfEntity> GetLisUfEntity()
        {
            var listEntity = new List<UfEntity>();

            for (var i = 0; i < 5; i++)
            {
                var item = new UfEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                listEntity.Add(item);
            }

            return listEntity;
        }
    }
}
