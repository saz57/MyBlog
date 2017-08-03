using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Enums;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public UserRole Role { get; set; }
        public string NickName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public CurrentUserViewModel CurrentUser { get; set; }

        public UserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Role = user.Role;
            NickName = user.NickName;
            Posts = user.Posts;
            Comments = user.Comments;        
        }

    }
}