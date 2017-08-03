using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AutorId { get; set; }
        public string Content { get; set; }
    }
}