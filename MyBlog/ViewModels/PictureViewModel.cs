using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.ViewModels
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public bool ToDelete { get; set; }
        public string PicturePath { get; set; }
        public string MimeType { get; set; }
        public int PostId { get; set; }
    }
}