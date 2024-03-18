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

        #region Index
        public async Task<IActionResult> Index()
        {
            string loggerSignature = "ActorsController ==> Index : ";
            var allActors = await _service.GetAllAsync();
            logger.LogInformation($"{loggerSignature} get all actors from database successfully");
            return View(allActors);
        }
        #endregion


        #region create 

        /* Create new actor
         * go to form post create
         */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /* go to form post 
            * check the model if vaild from user 
            * if no return the same view with the model error 
            * if yes AddAsync the new actor
            */
        [HttpPost]
        public async Task<IActionResult> Create(Actor newActor)
        {
            string loggerSignature = "ActorsController ==> Create Post : ";
            if (!ModelState.IsValid)
            {
                return View(newActor);
            }
            logger.LogError($"{loggerSignature} is not valid model");
             await _service.AddAsync(newActor);
            logger.LogInformation($"{loggerSignature} is added successfully");
            return RedirectToAction("Index");
        }
        #endregion

        #region Details

        //Details
        public async Task<IActionResult> Details (int id)
        {
           var actor = await _service.GetByIdAsync(id);
            if(actor is null)
                return View("NotFound");
            
            return View(actor);
        }

        #endregion


        #region Edit

        /* 1* get the actor id to check if there is an actor with this id
         * if No return view Notfound
         * if yesreturn a view with form post to get the update 
         */
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor is null)
                return View("NotFound");
            return View(actor);
        }


        /* 2* get the id and the new actor parameters the changed from user 
         * the actor param is will use to return the same view if the model is not vaild 
         * if model vaild will go to UpdateAsync to update the actor 
         */
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);
            await _service.UpdateAsync(actor);
            return RedirectToAction("Index");

        }

        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete (int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor is null)
                return View("NotFound");
            return View(actor);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*check
             * if no return not found 
             * if yes will delte
             */
 
            await _service.DeleteAsync(id);
            return RedirectToAction("index");

        }
        #endregion 

    }
}