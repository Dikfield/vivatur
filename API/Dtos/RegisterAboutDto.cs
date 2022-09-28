using API.Entities;

namespace API.Dtos
{
    public class RegisterAboutDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool VivaInfo { get; set; }
    }
}
