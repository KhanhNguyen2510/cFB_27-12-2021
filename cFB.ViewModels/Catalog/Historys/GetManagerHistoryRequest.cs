using cFB.Data.Enums;
using cFB.ViewModels.Common;
using System;

namespace cFB.ViewModels.Catalog.Historys
{
    public class GetManagerHistoryRequest : PagingRequestBase
    {
        public string AdministrativeDivision_Id { get; set; }
        public Event? Event { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
