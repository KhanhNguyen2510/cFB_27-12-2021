using System;
using System.Collections.Generic;

namespace cFB.Data.Entites
{
    public class AdministrativeDivision
    {
        public string AdministrativeDivisionId { get; set; }
        public string AdministrativeDivisionName { get; set; }
        public string NumberPhone { get; set; }
        public string Addrees { get; set; }
        public DateTime TimeOnline { get; set; }
        public string Password { get; set; }
        public string ManagerId { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<WatchList> WatchLists { get; set; }
        public IEnumerable<History> Histories { get; set; }
        public RoleManager RoleManager { get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public IEnumerable<HistoryClient> HistoryClients { get; set; }
    }
}
