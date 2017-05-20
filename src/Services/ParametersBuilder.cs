using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ParametersBuilder<T>where T : class
    {
        private readonly ParametersBuilderFluent<T> _set;

        public ParametersBuilder(IConsumer consumer, string resourcePath)
        {
            _set = new ParametersBuilderFluent<T>(this, consumer, resourcePath);

        }

        public ParametersBuilderFluent<T> Set
        {
            get { return _set;  }
        }
        public int Page { get; set; }
        public int Limit { get; set; }
        public Dictionary<string, string> Filters { get; set; }
        public string Order { get; set; }
    }
}
