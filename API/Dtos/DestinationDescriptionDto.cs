using API.Entities;

namespace API.Dtos
{
    public class DestinationDescriptionDto:Description
    {
        public string PhotoUrl { get; set; }
        public int PhotoId { get; set; }
        public Destination Destination { get; set; }
        public int DestinationId { get; set; }
    }
}
