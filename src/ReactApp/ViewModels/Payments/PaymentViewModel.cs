using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels.Payments
{
    public class PaymentViewModel
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
    }
}
