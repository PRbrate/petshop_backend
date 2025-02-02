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
                    response.Errors =  JsonConvert.DeserializeObject<ExpandoObject>(contentResponse);
                }
                return response;
            }
        }
    }
}
