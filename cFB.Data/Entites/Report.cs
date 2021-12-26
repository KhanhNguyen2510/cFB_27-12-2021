using System;

namespace cFB.Data.Entites
{
    public class Report
    {
        public string ReportId { get; set; }
        public string FileReport { get; set; }
        public DateTime DateReport { get; set; }
        public string AdministrativeDivisionId { get; set; }
        public string PostId { get; set; }
        public Post Posts { get; set; }
        public AdministrativeDivision AdministrativeDivision { get; set; }
    }
}
