using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Autor { get; set; }
        public string Content { get; set; }
    }
}