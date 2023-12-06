namespace Application.DTOs.Photo
{
    using Microsoft.AspNetCore.Http;

    public class MainPhotoDto
    {
        public IFormFile File { get; set; } = null!;

        public bool IsMain { get; set; }
    }
}
