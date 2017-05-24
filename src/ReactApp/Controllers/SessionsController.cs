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
        private readonly IMapper _mapper;

        public SessionsController
            (
            IRepositoryService<Session> sessionService, 
            IRepositoryService<Item> itemsService,
            IRepositoryService<Payment> paymenstService,
            IMapper mapper
            )
        {
            _sessionService = sessionService;
            _itemsService = itemsService;
            _paymentsService = paymenstService;
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


            SessionDetailViewModel viewModel = new SessionDetailViewModel();
            try
            {   
                var items = await _itemsService.GetAllDetails("sessions", id);
                var session = await _sessionService.GetById(id);                

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
                viewModel.PaymentMade = await GetPaymentsMade(id);
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

        private async Task<double> GetPaymentsMade(string id)
        {
            var payment = await _paymentsService.GetAllDetails("sessions", id);
            payment.Sum(p => p.Amount);
            return payment.Sum(p => p.Amount) / 100.0;
        }


    }
}
