﻿using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationships

        // Cinema
        //optional
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        //must
        public Cinema? Cinema { get; set; }

        // Producer
        //optional
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        //must
        public Producer? Producer { get; set; }  
        //Actors_Movies
        public ICollection<Actor_Movie> Actors_Movies { get; set; } = new HashSet<Actor_Movie>();
        
    }
}
