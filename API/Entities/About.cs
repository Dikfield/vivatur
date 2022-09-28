namespace API.Entities
{
    public class About
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool VivaInfo { get; set; }
        public ICollection<VivaPhoto> VivaPhotos { get; set; }
    }
}
