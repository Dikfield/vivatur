namespace API.Entities
{
    public class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Public { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? City { get; set; }
        public string? BestMonths { get; set; }
        public ICollection<DestinationPhoto> DestinationPhotos { get; set; }

    }
}
