using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("VivaPhotos")]
    public class VivaPhoto:Photo
    {
        public About About{ get; set; }
        public int AboutId{ get; set; }
    }
}
