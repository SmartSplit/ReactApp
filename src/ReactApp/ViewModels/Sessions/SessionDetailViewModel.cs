using Models;
using ReactApp.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels.Sessions
{
    public class SessionDetailViewModel
    {
        public int ItemCount { get; set; }
        public double PaymentMade { get; set; }
        public double PurchasesMade { get; set; }
        public SessionViewModel Session { get; set; }
        public ItemListViewModel Items { get; set; }
        public Dictionary<User, double> paymentsByUser { get; set; }


    }
}
