using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Models;
namespace MyBlog.Repository
{
    interface IRepository
    {
        List<Post> GetPosts(int pageNumber, int pageSize);
        Post GetPost(int id);
        Comment GetComment(int id);
        void Put(Post item);
        void Put(Comment item);
        void DeletePostById(int id);
        void DeleteCommentById(int id);
        void Update(Post item);
        void Update(Comment item);
    }
}
