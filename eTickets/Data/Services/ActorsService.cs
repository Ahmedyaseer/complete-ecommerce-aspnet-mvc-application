using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var allActors = await _context.Actors.ToListAsync();
            return allActors;
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a=>a.Id == id);
            return actor; 
        }

        public void Add(Actor actor)
        {
            _context.Add(actor);
            _context.SaveChanges();
        }


        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
           var actor = _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            _context.Update(newActor);
            _context.SaveChangesAsync();
            return newActor;
        }

        public void Delete(Actor actor)
        {
            _context.Remove(actor);
            _context.SaveChanges();
        }
    }
}
