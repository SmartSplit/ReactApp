using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ParametersBuilder
    {
        private readonly ParametersBuilderFluent _set;

        public ParametersBuilder()
        {
            Filters = new List<string>();
            _set = new ParametersBuilderFluent(this);
        }

        public ParametersBuilderFluent Set
        {
            get { return _set;  }
        }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public List<string> Filters { get; set; }
        public string Order { get; set; }

        public string getUrl()
        {
            return (PageParam() + LimitParam() + FiltersParam() + OrderParam()).Substring(1);
        }

        public override string ToString()
        {
            return getUrl();
        }

        public string PageParam()
        {
            return Page != null ? "&page=" + Page.Value : "";
        }

        public string LimitParam()
        {
            return Limit != null ? "&limit=" + Limit.Value : "";
        }

        public string FiltersParam()
        {
            if (Filters != null)
            {
                string filtersString = "&filters=";
                foreach (var item in Filters)
                {
                    filtersString += item + ",";
                }

                return filtersString.Substring(0, filtersString.Length - 1);
            }
            else
            {
                return "";
            }
        }

        public string OrderParam()
        {
            return Order != null ? "&order=" + Order : "";
        }
    }
}
