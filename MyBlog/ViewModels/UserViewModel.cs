using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

using MyBlog.Models;


namespace MyBlog.ViewModels
{
    public class UserViewModel
    {
        public bool IsBlocked { get; set; }
        public string Id { get; set; }
        public string NickName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<string> Roles { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public UserViewModel(ApplicationUser user, ICollection<string> roles)
        {
            Id = user.Id;
            Roles = roles;
            NickName = user.UserName;
            Posts = user.Posts;
            Comments = user.Comments;
            RegistrationDate = user.RegistrationDate;
            IsBlocked = user.IsBlocked;
        }

    }
}