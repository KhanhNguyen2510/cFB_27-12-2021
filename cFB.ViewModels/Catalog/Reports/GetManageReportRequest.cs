using System;

namespace cFB.ViewModels.Catalog.Reports
{
    public class GetManageReportRequest
    {
        public string AdministrativeDivisionID { get; set; }
        public DateTime DateReport { get; set; }
        public string FileReport { get; set; }
        public string ReportID { get; set; }
        public string PostID { get; set; }
    }
}
