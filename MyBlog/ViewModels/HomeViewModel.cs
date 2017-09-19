using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;
using PagedList;

namespace MyBlog.ViewModels
{
    public class HomeViewModel
    {
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public IPagedList<Post> Posts { get; set; }
    }
}