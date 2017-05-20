using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ParametersBuilderFluent<T> where T : class
    {
        private readonly ParametersBuilder<T> _builder;
        protected IConsumer _consumer;
        protected string _resourcePath;

        public ParametersBuilderFluent(ParametersBuilder<T> builder, IConsumer consumer, string resourcePath)
        {
            _builder = builder;
            _consumer = consumer;
            _resourcePath = resourcePath;
        }

        public ParametersBuilderFluent<T> Limit(int limit)
        {
            _builder.Limit = limit;
            return this;
        }
        public ParametersBuilderFluent<T> Page(int page)
        {
            _builder.Page = page;
            return this;
        }

        public async Task<List<T>> Get()
        {
            var responseObject = await _consumer.MakeGetCall(_resourcePath + "?limit=1000");

            var users = JsonConvert.DeserializeObject<List<T>>((responseObject.data.ToString()));

            return users;
        }
    }
}
