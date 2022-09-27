using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("PromotionDescriptionPhotos")]
    public class PromotionDescriptionPhoto:Photo
    {
        public PromotionDescription Description { get; set; }
        public int PromotionDescriptionId { get; set; }
    }
}
