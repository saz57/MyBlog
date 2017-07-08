using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;

namespace MyBlog.Repository
{
    public class EntityRepository : IRepository
    {

       public List<Post> GetPosts(int pageNumber, int pageSize)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                List<Post> posts = new List<Post>();

                if (context.Posts.Count<Post>() >= pageNumber * pageSize)
                {
                    posts = context.Posts.ToList<Post>().GetRange((pageNumber - 1) * pageSize, pageSize);
                }

                if (context.Posts.Count<Post>() < pageNumber * pageSize)
                {
                    posts = context.Posts.ToList<Post>().GetRange((pageNumber - 1) * pageSize, context.Posts.Count<Post>());
                }

                foreach(Post post in posts)
                {
                    post.Comments = context.Comments.Where(x => x.PostId == post.PostId).ToList<Comment>();
                }

                return posts;
            }

        }

        public Post GetPost(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                return context.Posts.FirstOrDefault<Post>(x => x.PostId == id);
            }
        }

        public Comment GetComment(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                return context.Comments.FirstOrDefault<Comment>(x => x.CommentId == id);
            }
        }

        public void Put(Post item)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Posts.Add(item);
                context.SaveChanges();
            }
        }

        public void Put(Comment item)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Comments.Add(item);
                context.SaveChanges();
            }
        }

        public void DeletePostById(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Posts.Remove(context.Posts.FirstOrDefault(x => x.PostId == id));
                context.SaveChanges();
            }
        }

        public void DeleteCommentById(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Comments.Remove(context.Comments.FirstOrDefault(x => x.CommentId == id));
                context.SaveChanges();
            }
        }

        public void Update(Post item)
        {
            using (BlogContext context = new BlogContext())
            {
                Post post = context.Posts.FirstOrDefault<Post>(x => x.PostId == item.PostId);

                if (post != null)
                {
                    post.Autor = item.Autor;
                    post.Content = item.Content;
                    context.SaveChanges();
                }

            }
        }

        public void Update(Comment item)
        {
            using (BlogContext context = new BlogContext())
            {
                Comment comment = context.Comments.FirstOrDefault<Comment>(x => x.CommentId == item.CommentId);

                if (comment != null)
                {
                    comment.Autor = item.Autor;
                    comment.Content = item.Content;
                    context.SaveChanges();
                }

            }
        }
    }
}