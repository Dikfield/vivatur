using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("DestinationPhotos")]
    public class DestinationPhoto:Photo
    {
        public Destination Destination { get; set; }
        public int DestinationId { get; set; }
    }
}
