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
    public class UfGets : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UfGets(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "GET de UF")]
        [Trait("GET", "UfEntity")]
        public async Task When_GET_UF_Success()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repository = new UfImplementation(context);

                //A entidade abaixo tem que ter o mesmo dado da entidade criada no UfSeeds
                //pois será comparada com a que foi criada (UfSeeds)
                UfEntity _entity = new UfEntity
                {
                    Id = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7"),
                    Sigla = "RJ",
                    Name = "Rio de Janeiro",
                    CreateAt = DateTime.UtcNow
                };

                #region Teste para o método ExistAsync
                var _existRegister = await _repository.ExistAsync(_entity.Id);
                Assert.True(_existRegister);
                #endregion

                #region Teste para o método SelectAsync
                var _recordSelected = await _repository.SelectAsync(_entity.Id);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_entity.Sigla, _recordSelected.Sigla);
                Assert.Equal(_entity.Name, _recordSelected.Name);
                #endregion

                #region Teste para o método SelectAsync retornando todos os registros
                var _allRecords = await _repository.SelectAsync();
                Assert.NotNull(_allRecords);
                Assert.True(_allRecords.Count() == 27);
                #endregion
            }
        }
    }
}
