﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Services
{
    public class Consumer : IConsumer
    {
        private JWT token;
        private int _clientId;
        private string _clientSecret;
        private string _servicePath;
        private string _basePath;
        private string _tokenGenerationPath;
        private string _username;
        private string _password;
        private string _version;

        private static IConfigurationRoot Configuration { get; set; }

        public async static Task<Consumer> Create()
        {
            Consumer consumer = new Consumer();
            try
            {
                consumer.token = await consumer.GetAccessToken();
            }
            catch (Exception e)
            {

            }

            return consumer;
        }

        private Consumer()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var protocol = Configuration["protocol"];
            var baseUrl = Configuration["baseUrl"];

            _servicePath = protocol + "://" + baseUrl + "/";
            _version = Configuration["version"];
            _basePath = _servicePath + _version + "/";
            _clientId = Convert.ToInt32(Configuration["clientId"]);
            _clientSecret = Configuration["clientSecret"];
            _tokenGenerationPath = Configuration["tokenGenerationPath"];
            _username = Configuration["username"];
            _password = Configuration["password"];
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
                    username = _username,
                    password = _password
                };

                string stringData = JsonConvert.SerializeObject(postData);
                var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");


                var response = await client.PostAsync(_servicePath + _tokenGenerationPath, contentData);
                var contents = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<JWT>(contents);
            }
        }

        public async Task<ApiResponse> MakeGetCall(string path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client = await EnsureToken(client);

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var responseString = await client.GetStringAsync(_basePath + path);
                var responseObject = JsonConvert.DeserializeObject<ApiResponse>(responseString, settings);

                return responseObject;
            }
            catch (Exception e)
            {
                throw new ApiCallException("Error making api call at " + path);
            }
        }

        public async Task<HttpResponseMessage> MakePostCall(string path, HttpContent content)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);
                    var response = await client.PostAsync(_basePath + path, content);

                    return response;
                }
            }
            catch (Exception e)
            {
                throw new ApiCallException("Error making api call at " + path);
            }
        }

        public async Task<HttpResponseMessage> MakePutCall(string path, HttpContent content)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);
                    var response = await client.PutAsync(_basePath + path, content);

                    return response;
                }
            }
            catch (Exception e)
            {
                throw new ApiCallException("Error making api call at " + path);
            }
        }

        private async Task<HttpClient> EnsureToken(HttpClient client)
        {
            if (this.token == null)
            {
                try
                {
                    this.token = await GetAccessToken();
                }
                catch (Exception e)
                {
                    return client;
                }
            }

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);

            return client;
        }
    }
}
