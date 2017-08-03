using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class UsersListViewModel
    {
        public bool HasNextPage { get; set; }
        public int CurrentPage { get; set; }
        public CurrentUserViewModel CurrentUser { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}