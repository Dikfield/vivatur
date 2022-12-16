using API.Entities;

namespace API.Dtos
{
    public class RegisterFeedDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FeedbackPhoto? FeedbackPhoto { get; set; }
    }
}
