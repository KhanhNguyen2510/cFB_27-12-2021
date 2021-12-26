using Microsoft.AspNetCore.Http;

namespace cFB.ViewModels.Catalog.Posts
{
    public class UploadFilePDFRequest
    {
        public string PostId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
