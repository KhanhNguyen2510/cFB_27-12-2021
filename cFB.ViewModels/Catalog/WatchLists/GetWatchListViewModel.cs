using cFB.Data.Enums;

namespace cFB.ViewModels.Catalog.WatchLists
{
    public class GetWatchListViewModel
    {
        public string FaceBookID { get; set; }
        public string FaceBookName { get; set; }
        public string FaceBookUrl { get; set; }
        public Status Status { get; set; }
        public string AdministrativeDivisionID { get; set; }
        public string AdministrativeDivisionName { get; set; }
        public string FaceBookTypeID { get; set; }
        public string FaceBookTypeName { get; set; }
    }
}
