using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Car
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string Model { get; init; }
        public int Year { get; init; }
        [Required]
        public string PictureUrl { get; init; }
        [Required]
        public string PlateNumber { get; init; }
        [Required]
        public string OwnerId { get; init; }
        public User Owner { get; init; }

        public ICollection<Issue> Issues { get; init; } = new HashSet<Issue>();
    }
}
