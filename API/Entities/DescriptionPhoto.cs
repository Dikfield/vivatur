using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("DescriptionPhotos")]
    public class DescriptionPhoto:Photo
    {
        public DestinationDescription Description { get; set; }
        public int DescriptionId { get; set; }
    }
}
