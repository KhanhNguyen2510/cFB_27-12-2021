using cFB.Data.Enums;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetWatchListInPythonRequest
    {
        public string AdministrativeDivisionID { get; set; }
        public string FaceBookTypeID { get; set; }
        public Status? Status { get; set; }
    }
}
