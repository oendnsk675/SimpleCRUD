﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Categories
    {  
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
