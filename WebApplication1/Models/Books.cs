using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?title { get; set; }

        public string ?Summary { get; set; }
    }
}
