using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class Post : BaseModel
    {
        public bool IsHidden { get; set; } 
        public string Content { get; set; }
        public ApplicationUser Autor { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}