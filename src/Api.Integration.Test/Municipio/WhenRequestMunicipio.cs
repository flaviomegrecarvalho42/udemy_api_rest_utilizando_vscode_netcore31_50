using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Municipio
{
    public class WhenRequestMunicipio : BaseIntegration
    {
        private string _name { get; set; }
        private int _codIBGE { get; set; }
        private Guid _ufId { get; set; }

        [Fact]
        public async Task Is_Possible_Realize_Crud_Municipio()
        {
            await AddToken();

            _name = Faker.Name.First();
            _codIBGE = Faker.RandomNumber.Next(1, 10000);
            _ufId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7");

            #region Criação do teste para o POST
            var municipioCreateDto = GetMunicipioCreateDto(_name, _codIBGE, _ufId);
            var response = await PostJsonAsync(municipioCreateDto, $"{hostApi}municipios", client);

            var postResult = await response.Content.ReadAsStringAsync();
            var postRegister = JsonConvert.DeserializeObject<MunicipioCreateResultDto>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, postRegister.Name);
            Assert.Equal(_codIBGE, postRegister.CodIBGE);
            Assert.Equal(_ufId, postRegister.UfId);
            #endregion

            #region Criação do teste para o GETALL
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Count() > 0);
            Assert.True(listFromJson.Where(r => r.Id == postRegister.Id).Count() == 1);
            #endregion

            #region Criação do teste para o PUT
            var municipioUpdateDto = GetMunicipioUpdateDto(postRegister.Id);
            var stringContent = new StringContent(JsonConvert.SerializeObject(municipioUpdateDto),
                                                  Encoding.UTF8, "application/json");

            response = await client.PutAsync($"{hostApi}municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var putRegister = JsonConvert.DeserializeObject<MunicipioUpdateResultDto>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(municipioUpdateDto.Name, putRegister.Name);
            Assert.Equal(municipioUpdateDto.CodIBGE, putRegister.CodIBGE);
            Assert.Equal(municipioUpdateDto.UfId, putRegister.UfId);
            #endregion

            #region Criação do teste para o GET Id
            response = await client.GetAsync($"{hostApi}municipios/{putRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var selectedRegister = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
            Assert.NotNull(selectedRegister);
            Assert.Equal(putRegister.Name, selectedRegister.Name);
            Assert.Equal(putRegister.CodIBGE, selectedRegister.CodIBGE);
            Assert.Equal(putRegister.UfId, selectedRegister.UfId);
            #endregion

            #region Criação do teste para o GET Complete/Id
            response = await client.GetAsync($"{hostApi}municipios/complete/{putRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var selectedRegisterComplete = JsonConvert.DeserializeObject<MunicipioCompletoDto>(jsonResult);
            Assert.NotNull(selectedRegisterComplete);
            Assert.Equal(putRegister.Name, selectedRegisterComplete.Name);
            Assert.Equal(putRegister.CodIBGE, selectedRegisterComplete.CodIBGE);
            Assert.Equal(putRegister.UfId, selectedRegisterComplete.UfId);
            Assert.NotNull(selectedRegisterComplete.Uf);
            #endregion

            #region Criação do teste para o GET ByIBGE/CodIBGE
            response = await client.GetAsync($"{hostApi}municipios/byIBGE/{putRegister.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var selectedRegisterByIBGE = JsonConvert.DeserializeObject<MunicipioCompletoDto>(jsonResult);
            Assert.NotNull(selectedRegisterByIBGE);
            Assert.Equal(putRegister.Name, selectedRegisterByIBGE.Name);
            Assert.Equal(putRegister.CodIBGE, selectedRegisterByIBGE.CodIBGE);
            Assert.Equal(putRegister.UfId, selectedRegisterByIBGE.UfId);
            Assert.NotNull(selectedRegisterByIBGE.Uf);
            #endregion

            #region Criação do teste para o DELETE
            response = await client.DeleteAsync($"{hostApi}municipios/{selectedRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await client.GetAsync($"{hostApi}municipios/{selectedRegister.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            #endregion
        }

        private static MunicipioCreateDto GetMunicipioCreateDto(string name, int codIBGE, Guid ufId)
        {
            return new MunicipioCreateDto()
            {
                Name = name,
                CodIBGE = codIBGE,
                UfId = ufId
            };
        }

        private static MunicipioUpdateDto GetMunicipioUpdateDto(Guid id)
        {
            return new MunicipioUpdateDto()
            {
                Id = id,
                Name = "Porto Velho",
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = new Guid("924e7250-7d39-4e8b-86bf-a8578cbf4002")
            };
        }
    }
}
