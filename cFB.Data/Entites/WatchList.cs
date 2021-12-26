using cFB.Data.Enums;
using System.Collections.Generic;

namespace cFB.Data.Entites
{
    public class WatchList
    {
        public string FaceBookId { get; set; }
        public string FaceBookName { get; set; }
        public string FaceBookUrl { get; set; }
        public Status Status { get; set; }
        public string AdministrativeDivisionId { get; set; }
        public string FaceBookTypeId { get; set; }
        public FaceBookType FaceBookType { get; set; }
        public AdministrativeDivision AdministrativeDivisions { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
