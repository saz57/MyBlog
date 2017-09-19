using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using MyBlog.Models;
using MyBlog.DAL.Context;

namespace MyBlog.DAL.Repository
{
    public class PostRepository
    {
        private BlogContext _dbContext;

        public PostRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }


        

        public List<Post> Get(int pageNumber, int pageSize, bool includeData = true, bool includeHidden = false)
        {
            List<Post> posts = new List<Post>();

            if (includeData)
            {
                posts = _dbContext.Posts.Include(p => p.Autor).Include(p => p.Comments.Select(c => c.Autor)).Include(p => p.Pictures).Where<Post>(p => p.IsHidden != true).ToList<Post>();
            }

            if (!includeData)
            {
                posts = _dbContext.Posts.Where<Post>(p => p.IsHidden != true).ToList<Post>();
            }

            return posts;
        }

        public Post Get(int id, bool includeData = true)
        {

            if (includeData)
            {
                return _dbContext.Posts.Include(p => p.Autor).Include(p => p.Autor).Include(p => p.Comments.Select(c => c.Autor)).FirstOrDefault<Post>(x => x.Id == id);
            }

            return _dbContext.Posts.FirstOrDefault<Post>(x => x.Id == id);


        }

        public Post Put(Post item)
        {
            _dbContext.Posts.Add(item);
            _dbContext.SaveChanges();
            Post post = _dbContext.Entry<Post>(item).Entity;
            return post;
        }



        public void DeleteById(int id)
        {
            Post post = _dbContext.Posts.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                _dbContext.Pictures.RemoveRange(post.Pictures);
                _dbContext.Comments.RemoveRange(post.Comments);
                _dbContext.Posts.Remove(post);
                
                _dbContext.SaveChanges();
            }
        }


        public void Update(Post item, bool isHidden = false)
        {

            Post post = _dbContext.Posts.Include(p => p.Autor).Include(p => p.Comments).FirstOrDefault<Post>(x => x.Id == item.Id);

            if (post != null)
            {
                post.IsHidden = isHidden;
                post.Name = item.Name;
                post.Content = item.Content;
                _dbContext.SaveChanges();
            }
        }
    }
}