using System;
using System.ComponentModel.DataAnnotations;

namespace MamyCare.Entities
{
    public class Article
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }
    }
}