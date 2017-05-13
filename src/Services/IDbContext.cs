using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IConsumer
    {
        Task<ApiResponse> MakeCall(string path);
    }
}
