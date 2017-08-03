using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;
using System.Data.Entity;

namespace MyBlog.Repository
{
    public class PostRepository
    {
        public List<Post> Get(int pageNumber, int pageSize, bool includeData = true, bool includeHidden = false)
        {
            List<Post> posts = new List<Post>();

            using (BlogContext context = new BlogContext())
            {
                List<Post> bufferList;
                if (includeData)
                {
                    bufferList = context.PostsSet.Include(p => p.Autor).Include(p => p.Comments.Select(c => c.Autor)).Where<Post>(p => p.IsHidden != true).ToList<Post>();

                    if (context.PostsSet.Count<Post>() >= pageNumber * pageSize)
                    {
                        posts = bufferList.GetRange((pageNumber - 1) * pageSize, pageSize);
                    }

                    if (context.PostsSet.Count<Post>() < pageNumber * pageSize)
                    {
                        posts = bufferList.GetRange((pageNumber - 1) * pageSize, bufferList.Count);
                    }
                }

                if (!includeData)
                {
                    bufferList = context.PostsSet.Where<Post>(p => p.IsHidden != true).ToList<Post>();

                    if (context.PostsSet.Count<Post>() >= pageNumber * pageSize)
                    {
                        posts = bufferList.GetRange((pageNumber - 1) * pageSize, pageSize);
                    }

                    if (context.PostsSet.Count<Post>() < pageNumber * pageSize)
                    {
                        posts = bufferList.ToList<Post>().GetRange((pageNumber - 1) * pageSize, bufferList.Count);
                    }

                }
            }

            return posts;
        }

        public Post Get(int id, bool includeData = true)
        {
            using (BlogContext context = new BlogContext())
            {
                if (includeData)
                {
                    return context.PostsSet.Include(p => p.Autor).Include(p => p.Autor).Include(p => p.Comments.Select(c => c.Autor)).FirstOrDefault<Post>(x => x.Id == id);
                }

                return context.PostsSet.FirstOrDefault<Post>(x => x.Id == id);
            }

        }

        public void Put(Post item)
        {
            using (BlogContext context = new BlogContext())
            {
                context.UsersSet.Attach(item.Autor);
                context.PostsSet.Add(item);
                context.SaveChanges();
            }
        }



        public void DeleteById(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                context.PostsSet.Remove(context.PostsSet.FirstOrDefault(x => x.Id == id));
                context.SaveChanges();
            }
        }


        public void Update(Post item, bool isHidden = false)
        {
            using (BlogContext context = new BlogContext())
            {
                Post post = context.PostsSet.Include(p => p.Autor).Include(p => p.Comments).FirstOrDefault<Post>(x => x.Id == item.Id);

                if (post != null)
                {
                    post.IsHidden = isHidden;
                    post.Content = item.Content;
                    context.SaveChanges();
                }

            }
        }
    }
}