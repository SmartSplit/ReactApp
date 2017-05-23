using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IModel<T>
    {
        T Id { get; set; }

        string ResourcePath { get; set; }
    }
}
