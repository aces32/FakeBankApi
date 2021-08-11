using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.IntegrationTests.Base.Token.Token
{
    public static class Auth0TokenGenerator
    {
        public async static Task<Auth0ResponseModel> GenerateAuth0Token()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ConnectionClose = false;
            client.BaseAddress = new Uri($"https://dev-p-zm5f3q.us.auth0.com/oauth/token");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new Auth0RequestModel
            {
                grant_type = "client_credentials",
                client_id = "tGgB4XhOVzTIESMteqrNCTXKAYpTkIhC",
                client_secret = "81xJ2XgiM3YG1BQVR-_ZaFCk2Y4bAZ8uU0v73xJBgMIzAR3S4ASikAaY-TIQ5L9z",
                audience = "https://dev-p-zm5f3q.us.auth0.com/api/v2/"
            };

            var response = await client.PostAsync(client.BaseAddress, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Auth0ResponseModel>(responseString);

            return result;
        }
    }
}
