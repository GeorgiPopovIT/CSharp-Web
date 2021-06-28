using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Issue
    {
        public string Id { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public bool IsFixed { get; init; }
        [Required]
        public string CarId { get; init; }
        public Car Car { get; init; }
    }
}
