using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }


        #region Index
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllIncludeAsync(i=>i.Cinema);
            return View(allMovies);
        }
        #endregion

        public async Task<IActionResult> Filter(string searchString)
        {
            
                var allMovies = await _service.GetAllIncludeAsync(n => n.Cinema);

                if (string.IsNullOrEmpty(searchString))
                      return View("Index", allMovies);

            var filteredResult = allMovies.Where
                (n => n.Name.ToLower().Contains(searchString.ToLower()) ||
                n.Description.ToLower().Contains(searchString.ToLower()))
                .ToList();

                    return View("Index", filteredResult);

            
        }

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details (int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropValuesAsync();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");


            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovieVM)
        {
            if(!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropValuesAsync();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(newMovieVM);
            }


            await _service.AddNewMoiveAsync(newMovieVM);

            return RedirectToAction(nameof(Index));
            

        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var movie = await _service.GetMovieByIdAsync(id);
            if(movie == null)
                return View("NotFound");
            var response = new NewMovieVM
            {
                Id = movie.Id,
                Name = movie.Name,
                ImageURL = movie.ImageURL,
                Price = movie.Price,
                Description = movie.Description,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                MovieCategory =movie.MovieCategory, 
                ActorsIds = movie.Actors_Movies.Select(i=>i.ActorId).ToList()
            };
            var movieDropdownsData = await _service.GetNewMovieDropValuesAsync();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);


        }

        [HttpPost]
        public async Task<IActionResult> Edit (NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropValuesAsync();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index)); 
        }

        #endregion
    }
}
