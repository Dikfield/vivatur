using API.Entities;

namespace API.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AboutId { get; set; }
        public FeedbackPhoto? FeedbackPhoto{ get; set; }
    }
}
