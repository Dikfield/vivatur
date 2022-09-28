using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("VivaPhotos")]
    public class VivaPhoto:Photo
    {
        public bool VivaPic { get; set; }
        public About About{ get; set; }
        public int AboutId{ get; set; }
    }
}
