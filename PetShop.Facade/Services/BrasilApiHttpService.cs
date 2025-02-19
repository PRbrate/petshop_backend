using Newtonsoft.Json;
using PetShop.Core.Entities;
using PetShop.Facade.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetShop.Facade.Services
{
    public class BrasilApiHttpService : IBrasilApiHttpService
    {
        public async Task<Response<CnpjResponse>> GetCnpj(string cnpj)
            {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cnpj/v1/{cnpj}");
            var response = new Response<CnpjResponse>();

            using (var client = new HttpClient())
            {
                var responseBrasilApi = await client.SendAsync(request);
                var contentResponse = await responseBrasilApi.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<CnpjResponse>(contentResponse);

                if (responseBrasilApi.IsSuccessStatusCode)
                {
                    response.Success = true;
                    response.Data = objResponse;
                }
                else
                {
                    response.Success = false;
                    response.ExternalErrors = JsonConvert.DeserializeObject<ExpandoObject>(contentResponse);
                }
                return response;
            }
        }

        public async Task<Response<CepResponse>> GetCep(string cep)
        {
            cep = new string(cep.Where(char.IsDigit).ToArray());

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/cep/v1/{cep}");
            var response = new Response<CepResponse>();


            using (var client = new HttpClient())
            {
                var responseBrasilApi = await client.SendAsync(request);
                var contentResponse = await responseBrasilApi.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<CepResponse>(contentResponse);

                if (responseBrasilApi.IsSuccessStatusCode)
                {
                    response.Success = true;
                    response.Data = objResponse;
                }
                else
                {
                    response.Success = false;
                    response.ExternalErrors = JsonConvert.DeserializeObject<ExpandoObject>(contentResponse);
                }
                return response;
            }


        }
    }
}
