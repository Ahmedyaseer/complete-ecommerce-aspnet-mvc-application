using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMoiveDropdownsVM
    {
        public NewMoiveDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }

        public List<Producer> Producers { get; set; } = new List<Producer>();
        public List<Cinema> Cinemas { get; set; } = new List<Cinema>(); 
        public List<Actor> Actors { get; set; } = new List<Actor>();
    }
}
