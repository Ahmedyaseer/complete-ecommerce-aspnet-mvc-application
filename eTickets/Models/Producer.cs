using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [Display (Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture URL is required")]
        [RegularExpression(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$", ErrorMessage = "Please enter a valid URL")]
        public string ProfilePictureURL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Actor Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        [Display (Name ="Producer Name")]
        public string FullName { get; set; } = string.Empty;

        [Display (Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Biography must be between 3 and 50 chars")]
        public string Bio { get; set; } = string.Empty;

        // Relationships
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();    
    }
}
