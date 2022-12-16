using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("FeedbackPhotos")]
    public class FeedbackPhoto:Photo
    {
        public Feedback feedback { get; set; }
        public int FeedbackId { get; set; }
    }
}
