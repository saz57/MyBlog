using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;
using System.Data.Entity;

namespace MyBlog.Repository
{
    public class CommentRepository
    {
        public List<Comment> Get(bool includeData = true)
        {
            using (BlogContext context = new BlogContext())
            {

                if(includeData)
                {
                   return context.CommentsSet.Include(c => c.Autor).Include(c => c.Post).Include(c => c.Post).Where<Comment>(c=>c.IsHidden == false).ToList<Comment>();
                }

                return context.CommentsSet.Include(c => c.Autor).Include(c => c.Post).Where<Comment>(c => c.IsHidden == false).ToList<Comment>();
            }
        }

        public Comment Get(int id, bool includeData = true)
        {
            using (BlogContext context = new BlogContext())
            {

                if (includeData)
                {
                    context.CommentsSet.Include(c => c.Autor).Include(c => c.Post).FirstOrDefault<Comment>(x => x.Id == id);
                }

                return context.CommentsSet.FirstOrDefault<Comment>(x => x.Id == id);
            }
        }

        public void Put(Comment item)
        {
            using (BlogContext context = new BlogContext())
            {
                    context.PostsSet.Attach(item.Post);
                    context.UsersSet.Attach(item.Autor);
                    context.CommentsSet.Add(item);
                    context.SaveChanges();
            }
        }



        public void DeleteById(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                context.CommentsSet.Remove(context.CommentsSet.FirstOrDefault(x => x.Id == id));
                context.SaveChanges();
            }
        }


        public void Update(Comment item, bool isHiddel = false)
        {
            using (BlogContext context = new BlogContext())
            {
                Comment comment = context.CommentsSet.FirstOrDefault<Comment>(x => x.Id == item.Id);

                if (comment != null)
                {
                    comment.IsHidden = isHiddel;
                    comment.Content = item.Content;
                    context.SaveChanges();
                }

            }
        }
    }
}