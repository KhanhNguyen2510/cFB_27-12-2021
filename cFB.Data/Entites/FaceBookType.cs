using System.Collections.Generic;

namespace cFB.Data.Entites
{
    public class FaceBookType
    {
        public string FaceBookTypeId { get; set; }
        public string FaceBookTypeName { get; set; }
        public string Description { get; set; }
        public IEnumerable<WatchList> WatchLists { get; set; }
    }
}
