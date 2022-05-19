using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Cep
{
    public class WhenRequestCep : BaseIntegration
    {
        private string _cep { get; set; }
        private string _logradouro { get; set; }
        private string _numero { get; set; }
        private Guid _municipioId { get; set; }

        [Fact]
        public async Task Is_Possible_Realize_Crud_Cep()
        {
            await AddToken();

            #region Criar um registro de Município que será utilizado nos testes de CEP
            var municipioCreateDto = GetMunicipioCreateDto();
            var responsePostMunicipio = await PostJsonAsync(municipioCreateDto, $"{hostApi}municipios", client);

            var postMunicipioResult = await responsePostMunicipio.Content.ReadAsStringAsync();
            var postMunicipioRegister = JsonConvert.DeserializeObject<MunicipioCreateResultDto>(postMunicipioResult);
            #endregion

            _cep = Faker.RandomNumber.Next(1, 10000).ToString();
            _logradouro = Faker.Address.StreetAddress();
            _numero = Faker.RandomNumber.Next(1, 10000).ToString();
            _municipioId = postMunicipioRegister.Id;

            #region Criação do teste para o POST
            var cepCreateDto = GetCepCreateDto(_cep, _logradouro, _numero, _municipioId);
            var response = await PostJsonAsync(cepCreateDto, $"{hostApi}ceps", client);

            var postResult = await response.Content.ReadAsStringAsync();
            var postCepRegister = JsonConvert.DeserializeObject<CepCreateResultDto>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_cep, postCepRegister.Cep);
            Assert.Equal(_logradouro, postCepRegister.Logradouro);
            Assert.Equal(_numero, postCepRegister.Numero);
            Assert.Equal(_municipioId, postCepRegister.MunicipioId);
            #endregion

            #region Criação do teste para o PUT
            var municipioUpdateDto = GetCepUpdateDto(postCepRegister.Id, _municipioId);
            var stringContent = new StringContent(JsonConvert.SerializeObject(municipioUpdateDto),
                                                  Encoding.UTF8, "application/json");

            response = await client.PutAsync($"{hostApi}ceps", stringContent);
            var putResult = await response.Content.ReadAsStringAsync();
            var putRegister = JsonConvert.DeserializeObject<CepUpdateResultDto>(putResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(municipioUpdateDto.Cep, putRegister.Cep);
            Assert.Equal(municipioUpdateDto.Logradouro, putRegister.Logradouro);
            Assert.Equal(municipioUpdateDto.Numero, putRegister.Numero);
            #endregion

            #region Criação do teste para o GET Id
            response = await client.GetAsync($"{hostApi}ceps/{putRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var getByIdResult = await response.Content.ReadAsStringAsync();
            var selectedRegister = JsonConvert.DeserializeObject<CepDto>(getByIdResult);
            Assert.NotNull(selectedRegister);
            Assert.Equal(putRegister.Id, selectedRegister.Id);
            Assert.Equal(putRegister.Cep, selectedRegister.Cep);
            Assert.Equal(putRegister.Logradouro, selectedRegister.Logradouro);
            Assert.Equal(putRegister.Numero, selectedRegister.Numero);
            Assert.Equal(putRegister.MunicipioId, selectedRegister.MunicipioId);
            #endregion

            #region Criação do teste para o GET ByCep/Id
            response = await client.GetAsync($"{hostApi}ceps/byCep/{putRegister.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var getByCepResult = await response.Content.ReadAsStringAsync();
            var selectedByCepRegister = JsonConvert.DeserializeObject<CepDto>(getByIdResult);
            Assert.NotNull(selectedByCepRegister);
            Assert.Equal(putRegister.Id, selectedByCepRegister.Id);
            Assert.Equal(putRegister.Cep, selectedByCepRegister.Cep);
            Assert.Equal(putRegister.Logradouro, selectedByCepRegister.Logradouro);
            Assert.Equal(putRegister.Numero, selectedByCepRegister.Numero);
            Assert.Equal(putRegister.MunicipioId, selectedByCepRegister.MunicipioId);
            #endregion

            #region Criação do teste para o DELETE
            response = await client.DeleteAsync($"{hostApi}ceps/{selectedRegister.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await client.GetAsync($"{hostApi}ceps/{selectedRegister.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            #endregion
        }

        private static MunicipioCreateDto GetMunicipioCreateDto()
        {
            return new MunicipioCreateDto()
            {
                Name = Faker.Name.First(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
            };
        }

        private static CepCreateDto GetCepCreateDto(string cep, string logradouro, string numero, Guid municipioId)
        {
            return new CepCreateDto()
            {
                Cep = cep,
                Logradouro = logradouro,
                Numero = numero,
                MunicipioId = municipioId
            };
        }

        private static CepUpdateDto GetCepUpdateDto(Guid id, Guid municipioId)
        {
            return new CepUpdateDto()
            {
                Id = id,
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                MunicipioId = municipioId
            };
        }
    }
}
