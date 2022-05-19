using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class MunicipioCrudComplete : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public MunicipioCrudComplete(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Municipio")]
        [Trait("CRUD", "MunicipioEntity")]
        public async Task When_CRUD_Municipio_Is_Success()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repository = new MunicipioImplementation(context);

                MunicipioEntity _entity = new MunicipioEntity
                {
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                #region Teste para o método InsertAsync
                var _recordCreated = await _repository.InsertAsync(_entity);
                Assert.NotNull(_recordCreated);
                Assert.Equal(_entity.Name, _recordCreated.Name);
                Assert.Equal(_entity.CodIBGE, _recordCreated.CodIBGE);
                Assert.Equal(_entity.UfId, _recordCreated.UfId);
                Assert.False(_recordCreated.Id == Guid.Empty);
                #endregion

                #region Teste para o método UpdateAsync
                _entity.Name = Faker.Name.First();
                _entity.Id = _recordCreated.Id;
                var _recordUpdated = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_recordUpdated);
                Assert.Equal(_entity.Name, _recordUpdated.Name);
                Assert.Equal(_entity.CodIBGE, _recordUpdated.CodIBGE);
                Assert.Equal(_entity.UfId, _recordUpdated.UfId);
                Assert.Equal(_entity.Id, _recordUpdated.Id);
                #endregion

                #region Teste para o método ExistAsync
                var _recordExisted = await _repository.ExistAsync(_recordUpdated.Id);
                Assert.True(_recordExisted);
                #endregion

                #region Teste para o método SelectAsync
                var _recordSelected = await _repository.SelectAsync(_recordUpdated.Id);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_recordUpdated.Name, _recordSelected.Name);
                Assert.Equal(_recordUpdated.CodIBGE, _recordSelected.CodIBGE);
                Assert.Equal(_recordUpdated.UfId, _recordSelected.UfId);
                Assert.Null(_recordSelected.Uf);
                #endregion

                #region Teste para o método SelectAsync retornando todos os registros
                var _allRecords = await _repository.SelectAsync();
                Assert.NotNull(_allRecords);
                Assert.True(_allRecords.Count() > 0);
                #endregion

                #region Teste para o método GetCompleteByIBGE
                _recordSelected = await _repository.GetCompleteByIBGE(_recordUpdated.CodIBGE);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_recordUpdated.Name, _recordSelected.Name);
                Assert.Equal(_recordUpdated.CodIBGE, _recordSelected.CodIBGE);
                Assert.Equal(_recordUpdated.UfId, _recordSelected.UfId);
                Assert.NotNull(_recordSelected.Uf);
                #endregion

                #region Teste para o método GetCompleteById
                _recordSelected = await _repository.GetCompleteById(_recordUpdated.Id);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_recordUpdated.Name, _recordSelected.Name);
                Assert.Equal(_recordUpdated.CodIBGE, _recordSelected.CodIBGE);
                Assert.Equal(_recordUpdated.UfId, _recordSelected.UfId);
                Assert.NotNull(_recordSelected.Uf);
                #endregion

                #region Teste para o método DeleteAsync
                var _recordDeleted = await _repository.DeleteAsync(_recordSelected.Id);
                Assert.True(_recordDeleted);

                _allRecords = await _repository.SelectAsync();
                Assert.NotNull(_allRecords);
                Assert.True(_allRecords.Count() == 0);
                #endregion
            }
        }
    }
}
