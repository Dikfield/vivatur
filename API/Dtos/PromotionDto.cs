using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class PromotionDto
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
        public decimal Price { get; set; }
        public string? City { get; set; }
        public string? Month { get; set; }
        public ICollection<PromotionPhoto> PromotionPhotos { get; set; }
    }
}
