using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Comment : BaseModel
    {
        public bool IsHidden { get; set; }
        public Post Post { get; set; }
        public ApplicationUser Autor { get; set; }
        public string Content { get; set; }
    }
}