using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class CinemasService : EntityBaseRepositiory<Cinema>,ICinemasService
    {
        public CinemasService(AppDbContext context):base(context)   
        {

        }
    }
}
