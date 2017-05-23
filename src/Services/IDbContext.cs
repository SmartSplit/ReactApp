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
    }
}
