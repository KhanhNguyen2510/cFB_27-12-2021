using cFB.Data.Enums;
using cFB.ViewModels.Common;
using System;

namespace cFB.ViewModels.Catalog.Posts
{
    public class GetManagePostRequest : PagingRequestBase
    {
        public string Search { get; set; }
        public string AdministrativeDivisionID { get; set; }
        public string NewsLabelID { get; set; }
        public string SentimentLabelID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TimeCrawl { get; set; }
        public Reported? Report { get; set; }
        public string WatchListID { get; set; }
    }
}
