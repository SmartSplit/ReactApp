using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public enum ServiceResultStatus
    {
        [Description("Error")]
        Error = 0,

        [Description("Success")]
        Succes = 1,

        [Description("Warning")]
        Warrnig,


        [Description("Information")]
        Info,
    }
    public class ServiceResult
    {
        public ServiceResultStatus Result { get; set; }

        public ICollection<String> Messages { get; set; }

        public ServiceResult()
        {
            Result = ServiceResultStatus.Succes;
            Messages = new List<string>();
        }

    }
}
