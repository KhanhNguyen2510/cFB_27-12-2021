using cFB.Data.Enums;
using cFB.ViewModels.Common;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetManageWatchListRequest : PagingRequestBase
    {
        public string AdministrativeDivisionID { get; set; }
        public string FacebookTypeID { get; set; }
        public Status? Status { get; set; }
    }
}
