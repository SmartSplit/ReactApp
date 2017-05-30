using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using AutoMapper;
using ReactApp.ViewModels.Sessions;
using ReactApp.ViewModels.Items;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactApp
{
    public class SessionsController : Controller
    {
        IRepositoryService<Session> _sessionService;
        IRepositoryService<Item> _itemsService;
        IRepositoryService<Payment> _paymentsService;
        IRepositoryService<User> _usersService;
        private readonly IMapper _mapper;

        public SessionsController
            (
            IRepositoryService<Session> sessionService, 
            IRepositoryService<Item> itemsService,
            IRepositoryService<Payment> paymenstService,
            IRepositoryService<User> userService,
            IMapper mapper
            )
        {
            _sessionService = sessionService;
            _itemsService = itemsService;
            _paymentsService = paymenstService;
            _usersService = userService;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            _sessionService.GetBuilder().Limit(1000);
            try
            {
                var sessions = await _sessionService.GetAll();

                SessionListViewModel viewModel = new SessionListViewModel()
                {
                    Sessions = sessions.Select(s => _mapper.Map<Session, SessionViewModel>(s)).ToList()
                };

                return View(viewModel);
            }
            catch (ApiCallException e)
            {
                ViewBag.Error = true;
                return View();
            }
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == "")
            {
                return new BadRequestResult();
            }

            ViewBag.id = id;

            SessionDetailViewModel viewModel = new SessionDetailViewModel();
            try
            {   
                var items = await _itemsService.GetAllDetails("sessions", id);
                var session = await _sessionService.GetById(id);
                var payment = await _paymentsService.GetAllDetails("sessions", id);

                var sessionViewModel = _mapper.Map<Session, SessionViewModel>(session);
                
                ItemListViewModel itemsViewModel = new ItemListViewModel()
                {
                    Items = items.Select(i => _mapper.Map<Item, ItemViewModel>(i)).ToList()
                };

                if (session == null)
                {
                    return NotFound();
                }

                viewModel.Session = sessionViewModel;
                viewModel.ItemCount = items.Count();
                viewModel.Items = itemsViewModel;
                viewModel.PaymentMade = GetPaymentsMade(payment);
                viewModel.PurchasesMade = itemsViewModel.Items.Sum(i => i.Price);
            }            
            catch (ApiCallException e)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = e.Message;

                return View();
            }

            return View(viewModel);
        }

        private double GetPaymentsMade(List<Payment> payments)
        {            
            payments.Sum(p => p.Amount);
            return payments.Sum(p => p.Amount) / 100.0;
        }

        public async Task<IActionResult> loadUserPayments(string id)
        {
            var payments = await _paymentsService.GetAllDetails("sessions", id);
            Dictionary<User, double> paymentByUser = new Dictionary<User, double>();
            foreach (var userId in payments.Select(p => p.UserId))
            {
                var user = await _usersService.GetById(userId);
                paymentByUser.Add(user, payments.Where(u => u.UserId == userId).Sum(p => p.Amount) / 100.0);
            }
            MorrisDataBuilder morris = new MorrisDataBuilder();

            return Json(morris.dataForUserPayments(paymentByUser));
        }


    }
}
