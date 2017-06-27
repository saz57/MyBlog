using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;

namespace MyBlog.PostStorage
{
    public static class Storage
    {
        public static List<Post> _posts;

        static Storage()
        {
            _posts = new List<Post>();
        }
    }
}