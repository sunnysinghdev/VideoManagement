using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Models
{
    public class VideoDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Uri Uri { get {
                return new Uri(Url);
            }
        }
        
        public int Size { get; set; }

    }
}
