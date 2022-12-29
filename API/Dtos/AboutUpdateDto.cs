using API.Entities;

namespace API.Dtos
{
    public class AboutUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool VivaInfo { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
