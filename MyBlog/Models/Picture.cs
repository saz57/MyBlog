using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace MyBlog.Models
{
    public class Picture: BaseModel
    {
        public string PicturePath { get; set; }
        public string MimeType { get; set; }
        public virtual Post Post { get; set; }
    }
}