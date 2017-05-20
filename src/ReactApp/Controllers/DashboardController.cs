using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Services;
using Models;
using ReactApp.ViewModels.Dashboard;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactApp.Controllers
{
    public class DashboardController : Controller
    {
        IRepositoryService<User> _usersService;
        IRepositoryService<Session> _sessionService;
        private readonly IMapper _mapper;

        public DashboardController(
            IRepositoryService<User> usersService,
            IRepositoryService<Session> sessionsService,
            IMapper mapper)
        {
            _usersService = usersService;
            _sessionService = sessionsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel();
            viewModel.UsersCount = (await _usersService.GetAll()).Count;
            viewModel.SessionsCount = (await _sessionService.GetAll()).Count;

            return View(viewModel);
        }

        public async Task<IActionResult> LoadMorrisSessions()
        {
            var sessions = (await _sessionService.GetAll()).ToList();
            var sessionsStarted = sessions.OrderBy(x => x.StartDate).ToList();
            var sessionsEnded = sessions.Where(x => x.EndDate.HasValue).ToList();

            List<MorrisSessionViewModel> morrisData = new List<MorrisSessionViewModel>();

            for (var day = DateTime.Today.Date; day.Date >= DateTime.Today.AddDays(-30); day = day.AddDays(-1))
            {
                MorrisSessionViewModel viewModel = new MorrisSessionViewModel();
                viewModel.xKey = day.ToString("yyyy-MM-dd");
                viewModel.startedCount = sessionsStarted.Where(x => x.StartDate.Date == day.Date).Count();
                viewModel.endedCount = sessionsEnded.Where(x => x.EndDate.Value.Date == day.Date).Count();
                morrisData.Add(viewModel);
            }
            return Json(morrisData);
        }
    }
}
