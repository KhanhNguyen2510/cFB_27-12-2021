using cFB.Data.Enums;
using System;
using System.Collections.Generic;

namespace cFB.Data.Entites
{
    public class Post
    {
        public string PostId { get; set; }
        public string UserUrl { get; set; }
        public string PostUrl { get; set; }
        public string PostContent { get; set; }
        public string Image { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime CrawledTime { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComment { get; set; }
        public int TotalShare { get; set; }
        public string NewsLabelId { get; set; }
        public string FaceBookId { get; set; }
        public string SentimentLabelId { get; set; }
        public string AdministrativeDivisionId { get; set; }
        public Reported Report { get; set; }
        public string FilePDF { get; set; }
        public WatchList WatchList { get; set; }
        public NewsLabel NewsLabel { get; set; }
        public SentimentLabel SentimentLabel { get; set; }
        public AdministrativeDivision AdministrativeDivisions { get; set; }
        public IEnumerable<Report> Reports { get; set; }
    }
}
