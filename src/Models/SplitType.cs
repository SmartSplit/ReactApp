using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public enum SplitType
    {
        [Display(Name = "Per Person")]
        per_person,
        [Display(Name = "Per Product")]
        per_product
    }
}
