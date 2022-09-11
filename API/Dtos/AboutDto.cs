using API.Entities;

namespace API.Dtos
{
    public class AboutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public ICollection<VivaPhoto> VivaPhotos { get; set; }
    }
}
