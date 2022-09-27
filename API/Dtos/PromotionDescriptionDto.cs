using API.Entities;

namespace API.Dtos
{
    public class PromotionDescriptionDto : Description
    {
        public string PhotoUrl { get; set; }
        public int PhotoId { get; set; }
        public Promotion Promotion { get; set; }
        public int PromotionId { get; set; }
    }
}
