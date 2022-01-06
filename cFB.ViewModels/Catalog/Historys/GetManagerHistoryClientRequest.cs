using cFB.ViewModels.Common;
using System;

namespace cFB.ViewModels.Catalog.Historys
{
    public class GetManagerHistoryClientRequest : PagingRequestBase
    {
        public string AdministrativeDivision_Id { get; set; }
        public DateTime? Time { get; set; }
        public string IPAdress { get; set; }
    }
}
