using API.Entities;

namespace API.Dtos
{
    public class AboutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool VivaInfo { get; set; }
        public ICollection<VivaPhoto> VivaPhotos { get; set; }
        public ICollection<Feedback>? Feedbacks{ get; set; }
    }
}
