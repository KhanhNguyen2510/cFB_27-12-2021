using System;

namespace cFB.Data.Entites
{
    public class HistoryClient 
    {
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string AdministrativeDivisionID { get; set; }
        public string NameMachine { get; set; }
        public DateTime Time { get; set; }
        public AdministrativeDivision AdministrativeDivisions { get; set; }
    }
}
