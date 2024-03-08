using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        private readonly ILogger<ActorsController> logger;

        public ActorsController(IActorsService service , ILogger<ActorsController> logger)
        {
            _service = service;
            this.logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            string loggerSignature = "ActorsController ==> Index : ";
            var allActors = await _service.GetAllAsync();
            logger.LogInformation($"{loggerSignature} get all actors from database successfully");
            return View(allActors);
        }
        #region create 
        // Create new actor
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Actor newActor)
        {
            string loggerSignature = "ActorsController ==> Create Post : ";
            if (!ModelState.IsValid)
            {
                return View(newActor);
            }
            logger.LogError($"{loggerSignature} is not valid model");
            _service.Add(newActor);
            logger.LogInformation($"{loggerSignature} is added successfully");
            return RedirectToAction("Index");
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,Actor newActor)
        {
            var actor = await _service.GetByIdAsync(id);
            var updateWithNew =_service.UpdateAsync(id, newActor);
            return RedirectToAction("Index");

        }

        //Details
        public async Task<IActionResult> Details (int id)
        {
           var actor = await _service.GetByIdAsync(id);
            if(actor is null)
            {
                return View(StatusCodes.Status404NotFound);
            }
            return View(actor);
        }
    }
}