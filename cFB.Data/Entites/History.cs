using cFB.Data.Enums;
using System;

namespace cFB.Data.Entites
{
    public class History
    {
        public string AdministrativeDivisionId { get; set; }
        public DateTime Time { get; set; }
        public Event Event { get; set; }
        public string StatusHistory { get; set; }
        public AdministrativeDivision AdministrativeDivisions { get; set; }
    }
}
