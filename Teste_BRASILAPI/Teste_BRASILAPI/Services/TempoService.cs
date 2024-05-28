using System.Text.Json;
using Teste_BRASILAPI.Interfaces;
using Teste_BRASILAPI.Models;

namespace Teste_BRASILAPI.Services
{
    public class TempoService : ITempo
    {
        public async Task<TempoModel> BuscarCidade(string cidade)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cptec/v1/cidade/{cidade}");

           using (var client = new HttpClient() )
            {
                var responseBrasilApi = await client.SendAsync(request);
                var contentResponse = await responseBrasilApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<TempoModel>(contentResponse);


                if (responseBrasilApi.IsSuccessStatusCode)
                {
                    objResponse.Verificacao = true;
                    return objResponse;
                }
                else
                {
                    objResponse.ErrorMessage = "Por favor, informe uma Cidade válida";
                    return objResponse;
                }
            }


        }
        
        public async Task<Clima> Previsao(int cityCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{cityCode}");

            using (var client =  new HttpClient() ) {

                var responseBrasilApi = await client.SendAsync(request);
                var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<Clima>(contentResp);

                return objResponse;
            }

           
        }
    }
}
