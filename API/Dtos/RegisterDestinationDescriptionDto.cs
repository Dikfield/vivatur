using API.Entities;

namespace API.Dtos
{
    public class RegisterDestinationDescriptionDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public IFormFile File { get; set; }

    }
}
