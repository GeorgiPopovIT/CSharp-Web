using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Models
{
    public class Card
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Keyword { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        [Required]
        public string Description { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}