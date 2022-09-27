using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterPromotionDto
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public int Public { get; set; }
        public decimal Price { get; set; }
        public string? City { get; set; }
        public string? Month { get; set; }

    }
}
