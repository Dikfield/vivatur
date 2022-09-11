using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("VivaPhotos")]
    public class VivaPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public About About{ get; set; }
        public int AboutId{ get; set; }
    }
}
