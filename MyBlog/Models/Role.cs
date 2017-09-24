using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Role: BaseModel
    {
        public string Name { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}