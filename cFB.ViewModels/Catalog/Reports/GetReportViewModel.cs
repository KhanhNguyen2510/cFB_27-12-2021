using System;

namespace cFB.ViewModels.Catalog.Reports
{
    public class GetReportViewModel
    {
        public string AdministrativeDivisionName { get; set; }
        public DateTime Date { get; set; }
        public string DateFill { get; set; }
        public string ReportId { get; set; }
        public string FileReport { get; set; }
        public string PostId { get; set; }
    }
}
