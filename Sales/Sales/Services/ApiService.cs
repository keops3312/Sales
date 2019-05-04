

namespace Sales.Services
{
    using Newtonsoft.Json;
    using Sales.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    public class ApiService
    {
        public async Task<Response> GetList<T>(string urlBase,
            string prefix,string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}, {controller}";//string.Format("{0}{1}", prefix, controller);
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                //si falla
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSucces = false,
                        Message = answer,

                    };
                }
                //si es exitoso
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {

                    IsSucces = true,
                    Message = "Ok",
                    Result = list,

                };

            }
            catch (Exception ex)
            {

                return new Response
                {

                    IsSucces = false,
                    Message = ex.Message,

                };
            }

            
        }
    }
}
