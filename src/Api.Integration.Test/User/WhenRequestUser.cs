using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
    public class WhenRequestUser : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task Is_Possible_Realize_Crud_User()
        {
            await AddToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            #region Criação do teste para o POST
            var userCreateDto = GetUserCreateDto(_name, _email);
            var response = await PostJsonAsync(userCreateDto, $"{hostApi}users", client);

            var postResult = await response.Content.ReadAsStringAsync();
            var postRegister = JsonConvert.DeserializeObject<UserCreateResultDto>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, postRegister.Name);
            Assert.Equal(_email, postRegister.Email);
            #endregion

            #region Criação do teste para o GETALL
            response = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Count() > 0);
            Assert.True(listFromJson.Where(r => r.Id == postRegister.Id).Count() == 1);
            #endregion

            #region Criação do teste para o PUT
            var userUpdateDto = GetUserUpdateDto(postRegister.Id);
            var stringContent = new StringContent(JsonConvert.SerializeObject(userUpdateDto),
                                    Encoding.UTF8, "application/json");

            response = await client.PutAsync($"{hostApi}users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var putRegister = JsonConvert.DeserializeObject<UserUpdateResultDto>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userUpdateDto.Name, putRegister.Name);
            Assert.Equal(userUpdateDto.Email, putRegister.Email);
            #endregion

            #region Criação do teste para o GET Id
            response = await client.GetAsync($"{hostApi}users/{putRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var selectedRegister = JsonConvert.DeserializeObject<UserDto>(jsonResult);
            Assert.NotNull(selectedRegister);
            Assert.Equal(putRegister.Name, selectedRegister.Name);
            Assert.Equal(putRegister.Email, selectedRegister.Email);
            #endregion

            #region Criação do teste para o DELETE
            response = await client.DeleteAsync($"{hostApi}users/{selectedRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await client.GetAsync($"{hostApi}users/{selectedRegister.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            #endregion   
        }

        private static UserCreateDto GetUserCreateDto(string name, string email)
        {
            return new UserCreateDto()
            {
                Name = name,
                Email = email
            };
        }

        private static UserUpdateDto GetUserUpdateDto(Guid id)
        {
            return new UserUpdateDto()
            {
                Id = id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };
        }
    }
}
