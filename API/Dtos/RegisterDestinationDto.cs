using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDestinationDto
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public int Public { get; set; }
        public string? City { get; set; }
        public string? BestMonths { get; set; }

    }
}
