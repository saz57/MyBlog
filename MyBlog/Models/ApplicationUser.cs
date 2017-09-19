using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using MyBlog.DAL.Repository;


namespace MyBlog.Models
{
    public class ApplicationUser: IdentityUser
    {
        public bool IsBlocked { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public ApplicationUser()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}