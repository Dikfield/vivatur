using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("PromotionPhotos")]
    public class PromotionPhoto:Photo
    {

        public Promotion Promotion{ get; set; }
        public int PromotionId { get; set; }
    }
}
