using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string AutorId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public ICollection<PictureViewModel> Images { get; set; }

        public PostViewModel()
        {
            Images = new List<PictureViewModel>();
        }
    }
}