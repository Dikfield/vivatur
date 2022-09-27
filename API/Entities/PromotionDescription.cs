using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("PromotionDescriptions")]
    public class PromotionDescription : Description
    {
        public PromotionDescriptionPhoto? PromotionDescriptionPhoto { get; set; }
        public int PromotionId { get; set; }
    }
}
