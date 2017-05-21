using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using AutoMapper;
using ReactApp.ViewModels.Sessions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactApp
{
    public class SessionsController : Controller
    {
        IRepositoryService<Session> _sessionService;
        IRepositoryService<Item> _itemsService;
        private readonly IMapper _mapper;

        public SessionsController(IRepositoryService<Session> sessionService, IRepositoryService<Item> itemsService, IMapper mapper)
        {
            _sessionService = sessionService;
            _itemsService = itemsService;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
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
            var session = await _sessionService.GetById(id);

            var viewModel = _mapper.Map<Session, SessionViewModel>(session);

            try
            {   
                var items = await _itemsService.GetAllDetails("sessions", id);

                if (session == null)
                {
                    return NotFound();
                }
            }            
            catch (ApiCallException e)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = e.Message;

                return View();
            }

            return View(viewModel);
        }


    }
}
