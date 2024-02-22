using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public VideoInfo Info { get; set; }
    }
    public class VideoInfo
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDownloaded { get; set; }

    }
}
