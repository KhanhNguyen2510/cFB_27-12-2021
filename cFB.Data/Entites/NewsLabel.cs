using System.Collections.Generic;

namespace cFB.Data.Entites
{
    public class NewsLabel
    {
        public string NewsLabelId { get; set; }
        public string NewsLabelName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
