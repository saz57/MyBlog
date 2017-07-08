using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class HomeViewModel
    {
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public IEnumerable<Post> Posts { get; set; }

    }
}