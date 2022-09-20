using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("DescriptionPhotos")]
    public class DescriptionPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
        public DestinationDescription Description { get; set; }
        public int DescriptionId { get; set; }
    }
}
