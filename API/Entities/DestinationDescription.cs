using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("DestinationDescriptions")]
    public class DestinationDescription:Description
    {
        public DescriptionPhoto? DescriptionPhoto { get; set; }
        public int DestinationId { get; set; }
    }
}
