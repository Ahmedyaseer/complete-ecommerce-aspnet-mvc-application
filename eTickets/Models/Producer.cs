using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Display (Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; } = string.Empty;
        [Display (Name ="Producer Name")]
        public string FullName { get; set; } = string.Empty;
        [Display (Name = "Biography")]
        public string Bio { get; set; } = string.Empty;

        // Relationships
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();    
    }
}
