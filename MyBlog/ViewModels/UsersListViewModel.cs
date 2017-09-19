using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

using MyBlog.Models;


namespace MyBlog.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }
        public IPagedList<UserViewModel> Users { get; set; }
    }
}