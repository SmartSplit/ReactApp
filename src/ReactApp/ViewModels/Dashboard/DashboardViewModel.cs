using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int UsersCount { get; set; }
        public int SessionsCount { get; set; }
        public int PaymentsCount { get; set; }
        public int ItemsCount { get; set; }
    }
}
