using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Autor { get; set; }
        public string Content { get; set; }
    }
}