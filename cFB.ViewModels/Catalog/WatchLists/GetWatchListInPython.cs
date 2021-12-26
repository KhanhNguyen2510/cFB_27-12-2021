using cFB.Data.Enums;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetWatchListInPython
    {
        public string FaceBookID { get; set; }
        public string FaceBookName { get; set; }
        public string FaceBookUrl { get; set; }
        public string FaceBookTypeID { get; set; }
        public string AdministrativeDivisionID { get; set; }
        public Status? Status { get; set; }
    }
}
