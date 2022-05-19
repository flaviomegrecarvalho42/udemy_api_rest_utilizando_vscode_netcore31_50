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
    public class CepCrudComplete : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public CepCrudComplete(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de CEP")]
        [Trait("CRUD", "CepEntity")]
        public async Task When_CRUD_CEP_Is_Success()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                #region Criando um Municipio e realizando o teste de Insert
                MunicipioImplementation _repositoryMunicipio = new MunicipioImplementation(context);

                MunicipioEntity _entityMunicipio = new MunicipioEntity
                {
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                var _recordMunicipioCreated = await _repositoryMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_recordMunicipioCreated);
                Assert.Equal(_entityMunicipio.Name, _recordMunicipioCreated.Name);
                Assert.Equal(_entityMunicipio.CodIBGE, _recordMunicipioCreated.CodIBGE);
                Assert.Equal(_entityMunicipio.UfId, _recordMunicipioCreated.UfId);
                Assert.False(_recordMunicipioCreated.Id == Guid.Empty);
                #endregion

                #region Criando um Municipio Mock
                CepImplementation _repositoryCep = new CepImplementation(context);

                CepEntity _entityCep = new CepEntity
                {
                    Cep = "20.770-060",
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = "0 até 2000",
                    MunicipioId = _recordMunicipioCreated.Id
                };
                #endregion

                #region Teste para o método InsertAsync
                var _recordCreated = await _repositoryCep.InsertAsync(_entityCep);
                Assert.NotNull(_recordCreated);
                Assert.Equal(_entityCep.Cep, _recordCreated.Cep);
                Assert.Equal(_entityCep.Logradouro, _recordCreated.Logradouro);
                Assert.Equal(_entityCep.Numero, _recordCreated.Numero);
                Assert.Equal(_entityCep.MunicipioId, _recordCreated.MunicipioId);
                Assert.False(_recordCreated.Id == Guid.Empty);
                #endregion

                #region Teste para o método UpdateAsync
                _entityCep.Id = _recordCreated.Id;
                _entityCep.Logradouro = Faker.Address.StreetAddress();
                var _recordUpdated = await _repositoryCep.UpdateAsync(_entityCep);
                Assert.NotNull(_recordUpdated);
                Assert.Equal(_entityCep.Cep, _recordUpdated.Cep);
                Assert.Equal(_entityCep.Logradouro, _recordUpdated.Logradouro);
                Assert.Equal(_entityCep.Numero, _recordUpdated.Numero);
                Assert.Equal(_entityCep.MunicipioId, _recordUpdated.MunicipioId);
                Assert.True(_entityCep.Id == _recordUpdated.Id);
                #endregion

                #region Teste para o método ExistAsync
                var _recordExisted = await _repositoryCep.ExistAsync(_recordUpdated.Id);
                Assert.True(_recordExisted);
                #endregion

                #region Teste para o método SelectAsync
                var _recordSelected = await _repositoryCep.SelectAsync(_recordUpdated.Id);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_recordUpdated.Cep, _recordSelected.Cep);
                Assert.Equal(_recordUpdated.Logradouro, _recordSelected.Logradouro);
                Assert.Equal(_recordUpdated.Numero, _recordSelected.Numero);
                Assert.Equal(_recordUpdated.MunicipioId, _recordSelected.MunicipioId);
                #endregion

                #region Teste para o método SelectAsync passando CEP como parâmetro
                _recordSelected = await _repositoryCep.SelectAsync(_recordUpdated.Cep);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_recordUpdated.Cep, _recordSelected.Cep);
                Assert.Equal(_recordUpdated.Logradouro, _recordSelected.Logradouro);
                Assert.Equal(_recordUpdated.Numero, _recordSelected.Numero);
                Assert.Equal(_recordUpdated.MunicipioId, _recordSelected.MunicipioId);
                Assert.NotNull(_recordSelected.Municipio);
                Assert.NotNull(_recordSelected.Municipio.Uf);
                Assert.Equal(_entityMunicipio.CodIBGE, _recordSelected.Municipio.CodIBGE);
                Assert.Equal(_entityMunicipio.Name, _recordSelected.Municipio.Name);
                Assert.Equal(_entityMunicipio.Uf.Name, _recordSelected.Municipio.Uf.Name);
                Assert.Equal(_entityMunicipio.Uf.Sigla, _recordSelected.Municipio.Uf.Sigla);
                #endregion

                #region Teste para o método SelectAsync retornando todos os registros
                var _allRecords = await _repositoryCep.SelectAsync();
                Assert.NotNull(_allRecords);
                Assert.True(_allRecords.Count() > 0);
                #endregion

                #region Teste para o método DeleteAsync
                var _recordDeleted = await _repositoryCep.DeleteAsync(_recordSelected.Id);
                Assert.True(_recordDeleted);

                _allRecords = await _repositoryCep.SelectAsync();
                Assert.NotNull(_allRecords);
                Assert.True(_allRecords.Count() == 0);
                #endregion
            }
        }
    }
}
