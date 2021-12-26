using cFB.Data.Enums;
using System;

namespace cFB.ViewModels.Catalog.Historys
{
    public class GetHistoryViewModel
    {
        public string AdministrativeDivision_Id { get; set; }
        public string AdministrativeDivision_Name { get; set; }
        public DateTime Time { get; set; }
        public Event Event { get; set; }
        public string StatusHistory { get; set; }
    }
}
