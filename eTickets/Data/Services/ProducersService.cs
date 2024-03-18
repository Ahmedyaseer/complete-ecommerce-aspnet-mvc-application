using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class ProducersService : EntityBaseRepositiory<Producer>, IPoroducersService
    {
        public ProducersService(AppDbContext context):base(context)
        {

        }
    }
}
