using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ApiResponse
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public string next_page_url { get; set; }
        public string prev_page_url { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public object data { get; set; }

        public bool failed { get; set; }
    }
}
