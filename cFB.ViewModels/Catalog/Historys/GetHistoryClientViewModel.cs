using System;

namespace cFB.ViewModels.Catalog.Historys
{
    public class GetHistoryClientViewModel
    {
        public string AdministrativeDivisionName { get; set; }
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string NameMachine { get; set; }
        public DateTime Time { get; set; }
    }
}
