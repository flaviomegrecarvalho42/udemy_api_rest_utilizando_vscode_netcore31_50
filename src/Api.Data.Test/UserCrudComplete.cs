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
    public class UserCrudComplete : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UserCrudComplete(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Uauário")]
        [Trait("CRUD", "UserEntity")]
        public async Task When_CRUD_Usuario_Is_Success()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);

                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                #region Teste para o método InsertAsync
                var _recordCreated = await _repository.InsertAsync(_entity);
                Assert.NotNull(_recordCreated);
                Assert.Equal(_entity.Email, _recordCreated.Email);
                Assert.Equal(_entity.Name, _recordCreated.Name);
                Assert.False(_recordCreated.Id == Guid.Empty);
                #endregion

                #region Teste para o método UpdateAsync
                _entity.Name = Faker.Name.First();
                var _recordUpdated = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_recordUpdated);
                Assert.Equal(_entity.Email, _recordUpdated.Email);
                Assert.Equal(_entity.Name, _recordUpdated.Name);
                #endregion

                #region Teste para o método ExistAsync
                var _recordExisted = await _repository.ExistAsync(_recordUpdated.Id);
                Assert.True(_recordExisted);
                #endregion

                #region Teste para o método SelectAsync
                var _recordSelected = await _repository.SelectAsync(_recordUpdated.Id);
                Assert.NotNull(_recordSelected);
                Assert.Equal(_recordUpdated.Email, _recordSelected.Email);
                Assert.Equal(_recordUpdated.Name, _recordSelected.Name);
                #endregion

                #region Teste para o método SelectAsync retornando todos os registros
                var _recordsSelected = await _repository.SelectAsync();
                Assert.NotNull(_recordsSelected);
                Assert.True(_recordsSelected.Count() > 0);
                #endregion

                #region Teste para o método  DeleteAsync
                var _recordDeleted = await _repository.DeleteAsync(_recordSelected.Id);
                Assert.True(_recordDeleted);
                #endregion

                #region Teste para o método FindByLogin
                var _usuarioPadrao = await _repository.FindByLogin("flavio@mail.com");
                Assert.NotNull(_usuarioPadrao);
                Assert.Equal("flavio@mail.com", _usuarioPadrao.Email);
                Assert.Equal("Administrador", _usuarioPadrao.Name);
                #endregion
            }
        }
    }
}
