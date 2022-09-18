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
        public bool Public { get; set; }
        public string PhotoUrl { get; set; }

        public string? Title1 { get; set; }
        public string? Title2 { get; set; }
        public string? Title3 { get; set; }
        public string? Title4 { get; set; }
        public string? Title5 { get; set; }

        public string? Description1 { get; set; }
        public string? Description2 { get; set; }
        public string? Description3 { get; set; }
        public string? Description4 { get; set; }
        public string? Description5 { get; set; }

        public decimal? Price { get; set; }
        public DateTime Created { get; set; }
        public string? City { get; set; }
        public string? Hotel { get; set; }
        public string? Country { get; set; }
        public string? BestMonths { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
