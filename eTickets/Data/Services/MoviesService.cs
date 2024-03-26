using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepositiory<Movie> , IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context):base(context) 
        {
            _context = context;
        }


        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies)
                .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(i => i.Id == id);
            return movieDetails;
        }

        /* <Summery>
         * to get all actors,cinmas and producer in one object or one model to be able to pass it to view 
         * we can do it like this
         * var respone =  new NewMovieDropdownVM
         *  response.Actors = await _context.Actors.OrderBy(a=>a.FullName).ToListAsync();
            response.Cinemas = await _context.Cinemas.OrderBy(a=>a.Name).ToListAsync();
            response.Producers = await _context.Producers.OrderBy(a => a.FullName).ToListAsync();
         * return an object 
         */
        public async Task<NewMoiveDropdownsVM> GetNewMovieDropValuesAsync()
        {
            return new NewMoiveDropdownsVM
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(a => a.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(a => a.FullName).ToListAsync()
            };
     
        }

        /* <Summery>
         * params NewMovieVM works as a dto, the main reson is we have a list of actors ids 
         * create a new movie object to add it to database 
         * set the NewMovieVM to the new movie object 
         * then add this object and save
         * we still have ids of actors in list 
         * so we will iterate on movieData.ActorsIds 
         * to set actorIds to Actor_Movie List
         * then add each id and save changes
         * return Void Task
        */

        public async Task AddNewMoiveAsync(NewMovieVM movieData)
        {
            var newMovie = new Movie
            {
                Name = movieData.Name,
                Description = movieData.Description,
                Price = movieData.Price,    
                ImageURL = movieData.ImageURL,
                StartDate = movieData.StartDate,
                EndDate = movieData.EndDate,
                MovieCategory = movieData.MovieCategory,
                CinemaId = movieData.CinemaId,  
                ProducerId = movieData.ProducerId
            };
            await _context.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            // Add Movie Actors
            foreach (var actorId in movieData.ActorsIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId,
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        /* <Summery>
         * params NewMovieVM becouse it is the model passed from view (New movie updated from user)
         * get the existing movie from data base 
         * and update existing movie with the new movie from user 
         * need to remove the list of actors ids 
         * then readd the actors ids in Actors_Movie model 
         */

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

    }
}
