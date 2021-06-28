using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string Username { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
        public bool IsMechanic { get; set; }
    }
}
