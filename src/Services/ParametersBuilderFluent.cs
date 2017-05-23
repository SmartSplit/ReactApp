using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ParametersBuilderFluent
    {
        private readonly ParametersBuilder _builder;

        public ParametersBuilderFluent(ParametersBuilder builder)
        {
            _builder = builder;
        }

        public ParametersBuilderFluent Limit(int limit)
        {
            _builder.Limit = limit;
            return this;
        }
        public ParametersBuilderFluent Page(int page)
        {
            _builder.Page = page;
            return this;
        }

        public ParametersBuilderFluent Filter(string column, string operant, string val)
        {
            _builder.Filters.Add(column + operant + val);
            return this;
        }

        public ParametersBuilderFluent Order(string column, string direction = "desc")
        {
            _builder.Order = column + "," + direction;
            return this;
        }
    }
}
