using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp
{
    public class MorrisDataBuilder
    {
        public List<MorrisCharViewModel> dataForUsers(List<User> users)
        {
            List<MorrisCharViewModel> morrisData = new List<MorrisCharViewModel>();

            for (var day = DateTime.Today.Date; day.Date >= DateTime.Today.AddDays(-30); day = day.AddDays(-1))
            {
                MorrisCharViewModel viewModel = new MorrisCharViewModel();
                viewModel.xKey = day.ToString("yyyy-MM-dd");
                viewModel.yKey = users.Where(x => x.CreatedAt.Date == day.Date).Count();
                morrisData.Add(viewModel);
            }

            return morrisData;
        }

        public List<MorrisSessionViewModel> dataForSessions(List<Session> sessionsStarted, List<Session> sessionsEnded)
        {
            List<MorrisSessionViewModel> morrisData = new List<MorrisSessionViewModel>();

            for (var day = DateTime.Today.Date; day.Date >= DateTime.Today.AddDays(-30); day = day.AddDays(-1))
            {
                MorrisSessionViewModel viewModel = new MorrisSessionViewModel();
                viewModel.xKey = day.ToString("yyyy-MM-dd");
                viewModel.startedCount = sessionsStarted.Where(x => x.StartDate.Date == day.Date).Count();
                viewModel.endedCount = sessionsEnded.Where(x => x.EndDate.Value.Date == day.Date).Count();
                morrisData.Add(viewModel);
            }

            return morrisData;
        }

        public List<MorrisDonutViewModel> dataForPayments(List<Payment> payments)
        {
            List<MorrisDonutViewModel> morrisData = new List<MorrisDonutViewModel>();

            foreach (var payment in payments)
            {
                MorrisDonutViewModel viewModel = new MorrisDonutViewModel();
                viewModel.label = payment.User.Name + " (" + payment.Item.Name + ")";
                viewModel.value = Convert.ToDouble(payment.Amount) / 100;
                morrisData.Add(viewModel);
            }

            return morrisData;
        }

        public List<MorrisDonutViewModel> dataForUserPayments(Dictionary<User, double> userPayments)
        {
            List<MorrisDonutViewModel> morrisData = new List<MorrisDonutViewModel>();

            foreach (var payment in userPayments)
            {
                MorrisDonutViewModel viewModel = new MorrisDonutViewModel();
                viewModel.label = payment.Key.Name;
                viewModel.value = payment.Value;
                morrisData.Add(viewModel);
            }

            return morrisData;
        }
    }

    public class MorrisCharViewModel
    {
        public string xKey { get; set; }
        public int yKey { get; set; }
    }

    public class MorrisDonutViewModel
    {
        public string label { get; set; }
        public double value { get; set; }
    }

    public class MorrisSessionViewModel
    {
        public string xKey { get; set; }
        public int startedCount { get; set; }
        public int endedCount { get; set; }
    }
}
