namespace API.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public string? City { get; set; }
        public string? Month { get; set; }
        public ICollection<PromotionPhoto> PromotionPhotos { get; set; }
        public ICollection<PromotionDescription> PromotionDescriptions { get; set; }

    }
}
