using System;

namespace cFB.ViewModels.Catalog.Posts
{
    public class GetPostUpdateRequest
    {
        public string PostContent { get; set; }
        public DateTime CrawledTime { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComment { get; set; }
        public int TotalShare { get; set; }
        public string NewsLabelID { get; set; }
        public string SentimentLabelID { get; set; }
    }
}
