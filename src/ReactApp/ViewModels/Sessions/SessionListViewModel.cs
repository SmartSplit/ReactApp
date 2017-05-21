using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels.Sessions
{
    public class SessionListViewModel
    {
        public List<SessionViewModel> Sessions { get; set; }

        public SessionListViewModel(List<SessionViewModel> sessions)
        {
            this.Sessions = sessions;
        }

        public SessionListViewModel()
        {

        }
    }
}
