using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTestService
    {
        [Fact(DisplayName = "É possível mapear os modelos de CEP")]
        public void Is_Possible_Map_Models_Cep()
        {
            var model = GetCepModel();
            var listEntity = GetListCepEntity();

            #region  Model => Entity (ModelToEntityProfile)
            var entity = Mapper.Map<CepEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Cep, entity.Cep);
            Assert.Equal(model.Logradouro, entity.Logradouro);
            Assert.Equal(model.Numero, entity.Numero);
            Assert.Equal(model.MunicipioId, entity.MunicipioId);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);
            #endregion

            #region Entity => Dto (EntityToDtoProfile)
            var cepDto = Mapper.Map<CepDto>(entity);
            Assert.Equal(entity.Id, cepDto.Id);
            Assert.Equal(entity.Cep, cepDto.Cep);
            Assert.Equal(entity.Logradouro, cepDto.Logradouro);
            Assert.Equal(entity.Numero, cepDto.Numero);
            Assert.Equal(entity.MunicipioId, cepDto.MunicipioId);

            var cepCompletoToDto = Mapper.Map<CepDto>(listEntity.FirstOrDefault());
            Assert.Equal(listEntity.FirstOrDefault().Id, cepCompletoToDto.Id);
            Assert.Equal(listEntity.FirstOrDefault().Cep, cepCompletoToDto.Cep);
            Assert.Equal(listEntity.FirstOrDefault().Logradouro, cepCompletoToDto.Logradouro);
            Assert.Equal(listEntity.FirstOrDefault().Numero, cepCompletoToDto.Numero);
            Assert.Equal(listEntity.FirstOrDefault().MunicipioId, cepCompletoToDto.MunicipioId);
            Assert.NotNull(cepCompletoToDto.Cep);
            Assert.NotNull(cepCompletoToDto.Municipio);
            Assert.NotNull(cepCompletoToDto.Municipio.Uf);

            var listDto = Mapper.Map<List<CepDto>>(listEntity);
            Assert.True(listEntity.Count() == listDto.Count());
            for (var i = 0; i < listDto.Count(); i++)
            {
                Assert.Equal(listEntity[i].Id, listDto[i].Id);
                Assert.Equal(listEntity[i].Cep, listDto[i].Cep);
                Assert.Equal(listEntity[i].Logradouro, listDto[i].Logradouro);
                Assert.Equal(listEntity[i].Numero, listDto[i].Numero);
                Assert.Equal(listEntity[i].MunicipioId, listDto[i].MunicipioId);
            }

            var cepCreateResultDto = Mapper.Map<CepCreateResultDto>(entity);
            Assert.Equal(entity.Id, cepCreateResultDto.Id);
            Assert.Equal(entity.Cep, cepCreateResultDto.Cep);
            Assert.Equal(entity.Logradouro, cepCreateResultDto.Logradouro);
            Assert.Equal(entity.Numero, cepCreateResultDto.Numero);
            Assert.Equal(entity.MunicipioId, cepCreateResultDto.MunicipioId);
            Assert.Equal(entity.CreateAt, cepCreateResultDto.CreateAt);

            var cepUpdateResultDto = Mapper.Map<CepUpdateResultDto>(entity);
            Assert.Equal(entity.Id, cepCreateResultDto.Id);
            Assert.Equal(entity.Cep, cepUpdateResultDto.Cep);
            Assert.Equal(entity.Logradouro, cepUpdateResultDto.Logradouro);
            Assert.Equal(entity.Numero, cepUpdateResultDto.Numero);
            Assert.Equal(entity.MunicipioId, cepUpdateResultDto.MunicipioId);
            Assert.Equal(entity.UpdateAt, cepUpdateResultDto.UpdateAt);
            #endregion

            #region Dto => Model (DtoToModelProfile)
            var cepModel = Mapper.Map<CepModel>(cepDto);
            Assert.Equal(cepDto.Id, cepModel.Id);
            Assert.Equal(cepDto.Cep, cepModel.Cep);
            Assert.Equal(cepDto.Logradouro, cepModel.Logradouro);
            Assert.Equal(cepDto.Numero, cepModel.Numero);
            Assert.Equal(cepDto.MunicipioId, cepModel.MunicipioId);

            var cepCreateDto = Mapper.Map<CepCreateDto>(cepModel);
            Assert.Equal(cepModel.Cep, cepCreateDto.Cep);
            Assert.Equal(cepModel.Logradouro, cepCreateDto.Logradouro);
            Assert.Equal(cepModel.Numero, cepCreateDto.Numero);
            Assert.Equal(cepModel.MunicipioId, cepCreateDto.MunicipioId);

            var cepUpdateDto = Mapper.Map<CepUpdateDto>(cepModel);
            Assert.Equal(cepModel.Id, cepUpdateDto.Id);
            Assert.Equal(cepModel.Cep, cepUpdateDto.Cep);
            Assert.Equal(cepModel.Logradouro, cepUpdateDto.Logradouro);
            Assert.Equal(cepModel.Numero, cepUpdateDto.Numero);
            Assert.Equal(cepModel.MunicipioId, cepUpdateDto.MunicipioId);
            #endregion
        }

        private static CepModel GetCepModel()
        {
            return new CepModel
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                MunicipioId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }

        private static List<CepEntity> GetListCepEntity()
        {
            var listEntity = new List<CepEntity>();

            for (var i = 0; i < 5; i++)
            {
                var item = new CepEntity
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Municipio = new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        CodIBGE = Faker.RandomNumber.Next(1, 10000),
                        UfId = Guid.NewGuid(),
                        Uf = new UfEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }
                };

                listEntity.Add(item);
            }

            return listEntity;
        }
    }
}
