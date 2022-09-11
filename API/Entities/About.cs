namespace API.Entities
{
    public class About
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public ICollection<VivaPhoto> VivaPhotos { get; set; }
    }
}
