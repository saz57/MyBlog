using System.Collections.Generic;
using MyBlog.Enums;

namespace MyBlog.Models
{
    public class ApplicationUser: BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        public ApplicationUser()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }
    }
}