using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepositiory<Actor>, IActorsService
    {

        public ActorsService(AppDbContext context) : base (context) { }

    }
}
