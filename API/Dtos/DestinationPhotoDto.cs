using API.Entities;

namespace API.Dtos
{
    public class DestinationPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
    }
}
