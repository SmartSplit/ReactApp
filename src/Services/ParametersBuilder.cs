using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ParametersBuilder
    {
        public int Page { get; }
        public int Limit { get; }
        public Dictionary<string, string> Filters { get; }
        public string Order { get; }
    }
}
