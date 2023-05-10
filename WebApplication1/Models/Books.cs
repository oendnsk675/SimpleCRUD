using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        [MaxLength(20)]
        public string? Summary { get; set; }

        public int? Category_id { get; set; }
    }
}
