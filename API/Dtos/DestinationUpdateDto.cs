namespace API.Dtos
{
    public class DestinationUpdateDto
    {
        public bool? Public { get; set; }

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
        public string? City { get; set; }
        public string? Hotel { get; set; }
        public string? Country { get; set; }
        public string? BestMonths { get; set; }

    }
}
