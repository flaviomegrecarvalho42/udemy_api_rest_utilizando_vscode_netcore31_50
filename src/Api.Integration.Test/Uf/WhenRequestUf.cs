using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf
{
    public class WhenRequestUf : BaseIntegration
    {
        [Fact]
        public async Task Is_Possible_Realize_Crud_Uf()
        {
            await AddToken();

            #region Criação do teste para o GETALL
            response = await client.GetAsync($"{hostApi}ufs");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Count() == 27);
            Assert.True(listFromJson.Where(r => r.Sigla == "RJ").Count() == 1);
            #endregion

            #region Criação do teste para o GET Id
            var ufId = listFromJson.Where(r => r.Sigla == "RJ").FirstOrDefault().Id;
            var ufName = listFromJson.Where(r => r.Sigla == "RJ").FirstOrDefault().Name;
            var ufSigla = listFromJson.Where(r => r.Sigla == "RJ").FirstOrDefault().Sigla;

            response = await client.GetAsync($"{hostApi}ufs/{ufId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var selectedRegister = JsonConvert.DeserializeObject<UfDto>(jsonResult);
            Assert.NotNull(selectedRegister);
            Assert.Equal(ufId, selectedRegister.Id);
            Assert.Equal(ufName, selectedRegister.Name);
            Assert.Equal(ufSigla, selectedRegister.Sigla);
            #endregion
        }
    }
}
