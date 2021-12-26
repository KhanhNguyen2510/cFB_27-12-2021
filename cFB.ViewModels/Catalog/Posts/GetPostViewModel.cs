using cFB.Data.Enums;
using System;

namespace cFB.ViewModels.Catalog.Posts
{
    public class GetPostViewModel
    {
        public string PostID { get; set; }
        public string UserUrl { get; set; }
        public string PostUrl { get; set; }
        public string PostContent { get; set; }
        public string Image { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime CrawledTime { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComment { get; set; }
        public int TotalShare { get; set; }
        public string Facebook_ID { get; set; }
        public string FacebookName { get; set; }
        public string FacebookUrl { get; set; }
        public string NewsLabelID { get; set; }
        public string NewsLabelName { get; set; }
        public string SentimentLabelID { get; set; }
        public string SentimentLabelName { get; set; }
        public string AdministrativeDivisionName { get; set; }
        public string AdministrativeDivisionID { get; set; }
        public string FilePDF { get; set; }
        public Reported Report { get; set; }
    }
}
