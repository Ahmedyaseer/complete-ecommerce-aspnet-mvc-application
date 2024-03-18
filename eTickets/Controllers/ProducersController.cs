using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IPoroducersService _service;

        public ProducersController(IPoroducersService service )
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync(); 
            return View(allProducers);
        }

        #region Details
        public async Task<IActionResult> Details (int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer is null)
                return View("NotFound");  
            return View(producer);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if(producer is null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
           await _service.UpdateAsync(producer);
            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Delete (int id)
        {
            var producer = await _service.GetByIdAsync(id); 
            if( producer is null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        [HttpPost]
        [ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");   
        }
    }
}
