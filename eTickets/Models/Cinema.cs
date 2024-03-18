using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Logo is required")]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Please enter a valid URL")]
        [Display (Name="Cinema Logo")]
        public string Logo { get; set; } = string.Empty;

        [Display(Name="Cinema Name")]
        [Required(ErrorMessage = "Cinema Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 chars")]
        public string Name { get; set; } = string.Empty;

        [Display (Name="Description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 50 chars")]
        public string Description { get; set; } = string.Empty;

        // Relationships
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();        

    }
}
