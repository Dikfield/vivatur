using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DestinationDto
    {
        public int Id { get; set; }
        [Required] 
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool Public { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? City { get; set; }
        public string? BestMonths { get; set; }
        public ICollection<DestinationPhoto> DestinationPhotos { get; set; }
    }
}
