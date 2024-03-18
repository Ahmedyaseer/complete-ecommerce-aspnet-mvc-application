using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepositiory<Movie> , IMoviesService
    {
        public MoviesService(AppDbContext context):base(context) 
        {

        }
    }
}
