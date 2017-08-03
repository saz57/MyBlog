using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Enums;
using MyBlog.Models;
namespace MyBlog.ViewModels
{
    public class CurrentUserViewModel
    {
        public int Id { get; private set; }
        public UserRole Role { get; private set; }
        public string NickName { get; private set; }

        public CurrentUserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Role = user.Role;
            NickName = user.NickName;
        }
    }
}