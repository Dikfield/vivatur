namespace API.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FeedbackPhoto? FeedbackPhoto { get; set; }
        public About? About { get; set; }
        public int? AboutId { get; set; }
    }
}
