using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepositiory<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMoiveDropdownsVM> GetNewMovieDropValuesAsync();
        Task AddNewMoiveAsync(NewMovieVM movieData);
        Task UpdateMovieAsync(NewMovieVM movie); 
    }
}
