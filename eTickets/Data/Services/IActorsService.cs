using eTickets.Models;
using System.Collections;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
       Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);  
        void Add(Actor actor);  
        Task<Actor> UpdateAsync (int id, Actor newActor); 
        void Delete(Actor actor);
    }
}
