using cFB.Data.Enums;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetManageListWatchListPagingRequest
    {
        public string AdministrativeDivisionID { get; set; }
        public string FacebookTypeID { get; set; }
        public Status? Status { get; set; }
    }
}
