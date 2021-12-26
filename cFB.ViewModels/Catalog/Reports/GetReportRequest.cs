using cFB.ViewModels.Common;
using System;

namespace cFB.ViewModels.Catalog.Reports
{
    public class GetReportRequest : PagingRequestBase
    {
        public string AdministrativeDivisionID { get; set; }
        public DateTime? DateReport { get; set; }
        public string ReportID { get; set; }
        public string PostID { get; set; }
    }
}
