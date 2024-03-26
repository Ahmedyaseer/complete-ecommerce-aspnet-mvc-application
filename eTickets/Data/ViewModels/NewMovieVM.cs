using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie Name is required")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Movie Description is required")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price in $")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Movie Poster URL is required")]
        [Display(Name = "Movie Poster URL")]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Please enter a valid URL")]
        public string ImageURL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Strat Date is required")]
        [Display(Name = "Movie Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "Movie End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Movie Category is required")]
        [Display(Name = "Select a Category")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships

        // Cinema
        [Required(ErrorMessage = "Movie Cinema  is required")]
        [Display(Name = "Select a Cinema")]
        public int CinemaId { get; set; }


        // Producer
        [Required(ErrorMessage = "Movie producer is required")]
        [Display(Name = "Select Producer ")]
        public int ProducerId { get; set; }

        //Actors_Movies
        [Required(ErrorMessage = "Movie Actor(s) is required")]
        [Display(Name = "Select Actor(s)")]
        public List<int> ActorsIds { get; set; } = new List<int>();

    }
}
