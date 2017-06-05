using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Services;
using Models;
using ReactApp.ViewModels.Dashboard;
using ReactApp.Filters;

namespace ReactApp.Controllers
{
    [ApiAuthorized]
    public class DashboardController : Controller
    {
        IRepositoryService<User> _usersService;
        IRepositoryService<Session> _sessionsService;
        IRepositoryService<Payment> _paymentsService;
        IRepositoryService<Item> _itemsService;
        private readonly IMapper _mapper;

        public DashboardController(
            IRepositoryService<User> usersService,
            IRepositoryService<Session> sessionsService,
            IRepositoryService<Payment> paymentsService,
            IRepositoryService<Item> itemsService,

            IMapper mapper)
        {
            _usersService = usersService;
            _sessionsService = sessionsService;
            _paymentsService = paymentsService;
            _itemsService = itemsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(User user)
        {
            ViewBag.User = _usersService.GetLoggedUser();

            _sessionsService.GetBuilder()
                .Filter("start_date", ">", DateTime.Today.AddDays(-30).ToString())
                .Limit(1000);

            _usersService.GetBuilder()
                .Filter("created_at", ">", DateTime.Today.AddDays(-30).ToString())
                .Limit(1000);

            _paymentsService.GetBuilder()
                .Filter("created_at", ">", DateTime.Today.AddDays(-30).ToString())
                .Limit(1000);

            _itemsService.GetBuilder()
                .Filter("created_at", ">", DateTime.Today.AddDays(-30).ToString())
                .Limit(1000);


            DashboardViewModel viewModel = new DashboardViewModel();
            try
            {
                viewModel.UsersCount = (await _usersService.GetAll()).Count;
                viewModel.SessionsCount = (await _sessionsService.GetAll()).Count;
                viewModel.PaymentsCount = (await _paymentsService.GetAll()).Count;
                viewModel.ItemsCount = (await _itemsService.GetAll()).Count;
            }
            catch (ApiCallException e)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = e.Message;

                return View();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> LoadMorrisSessions()
        {
            _sessionsService.GetBuilder()
                .Filter("start_date", ">", DateTime.Today.AddDays(-30).ToString())
                .Limit(1000);

            var sessions = (await _sessionsService.GetAll()).ToList();
            var sessionsStarted = sessions.OrderBy(x => x.StartDate).ToList();
            var sessionsEnded = sessions.Where(x => x.EndDate.HasValue).ToList();
            MorrisDataBuilder morris = new MorrisDataBuilder();

            return Json(morris.dataForSessions(sessionsStarted, sessionsEnded));
        }

        public async Task<IActionResult> LoadMorrisPayments()
        {
            _paymentsService.GetBuilder()
                .Filter("created_at", ">", DateTime.Today.AddDays(-30).ToString())
                .Order("amount")
                .Limit(3);

            var payments = await _paymentsService.GetAll();
            foreach (var payment in payments)
            {
                payment.User = await _usersService.GetById(payment.UserId);
                payment.Item = await _itemsService.GetById(payment.ItemId);
            }

            MorrisDataBuilder morris = new MorrisDataBuilder();

            return Json(morris.dataForPayments(payments));
        }
    }
}
