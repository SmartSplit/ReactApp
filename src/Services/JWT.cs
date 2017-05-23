using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class JWT
    {
        public int expires_in { get; set; }
        public string access_token { get; set; }

        public JWT(int expires_in, string access_token)
        {
            this.expires_in = expires_in;
            this.access_token = access_token;
        }
    }
}
