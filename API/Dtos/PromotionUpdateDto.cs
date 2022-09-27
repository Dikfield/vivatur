namespace API.Dtos
{
    public class PromotionUpdateDto
    {

        public bool Public { get; set; }
        public string? City { get; set; }
        public decimal Price { get; set; }
        public string? Month { get; set; }

    }
}
