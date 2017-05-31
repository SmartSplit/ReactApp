using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public interface IConsumer
    {
        Task<ApiResponse> MakeGetCall(string path);
        Task<HttpResponseMessage> MakePutCall(string path, HttpContent content);
        Task<HttpResponseMessage> MakePostCall(string path, HttpContent content);
        Task<bool> FetchCurrentUser();
        IAuthenticable GetUser();
        Task<JWT> GetUserAccessToken(IAuthenticable attemptingUser = null);
        Task<JWT> GetClientAccessToken();
        void SetToken(JWT token);
        void Logout();
    }
}
