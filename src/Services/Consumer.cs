using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Consumer
    {
        private JWT token;
        private int _clientId;
        private string _clientSecret;
        private string _servicePath;

        public async static Task<Consumer> Create(string servicePath, int clientId, string clientSecret)
        {
            Consumer consumer = new Consumer(servicePath, clientId, clientSecret);
            consumer.token = await consumer.GetAccessToken();

            return consumer;
        }

        private Consumer(string servicePath, int clientId, string clientSecret)
        {
            _servicePath = "https://api.smartsplit.eu/";
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
        
        public async Task<JWT> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var postData = new {
                    grant_type = "password",
                    client_id = _clientId,
                    client_secret = _clientSecret,
                    username = "test@example.com",
                    password = "qazwsxedc"
                };

                string stringData = JsonConvert.SerializeObject(postData);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_servicePath + "oauth/token", contentData);
                var contents = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<JWT>(contents);
            }
        }

        public async Task<string> MakeCall(string path)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);

                    return await client.GetStringAsync(_servicePath + "v1/"+ path);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error making api call at " + path);
            }
        }
    }
}
