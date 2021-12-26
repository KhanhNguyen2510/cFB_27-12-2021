using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cFB.Data.Entites
{
    public class SentimentLabel
    {
        public string SentimentLabelId { get; set; }
        public string SentimentLabelName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
