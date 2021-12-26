using System;
using System.ComponentModel.DataAnnotations;

namespace cFB.ViewModels.Catalog.Posts
{
    public class GetPostCreateRequest
    {
        public string PostId { get; set; }
        [Url(ErrorMessage = "Bạn cần nhập thông tin này")]
        [Required]
        public string UserUrl { get; set; }
        [Required]
        [Url(ErrorMessage = "Bạn cần nhập thông tin này")]
        public string PostUrl { get; set; }
        public string PostContent { get; set; }
        public string Image { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? CrawledTime { get; set; }
        public int? TotalLikes { get; set; }
        public int? TotalComment { get; set; }
        public int? TotalShare { get; set; }
        [Required]
        public string FacebookID { get; set; }
        public string NewsLabelID { get; set; }
        public string SentimentLabelID { get; set; }
        public string AdministrativeDivisionId { get; set; }
        public string FilePDF { get; set; }
    }
}
