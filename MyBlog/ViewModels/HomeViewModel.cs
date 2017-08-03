using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class HomeViewModel
    {
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public CurrentUserViewModel CurrentUser { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}