using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;

using MyBlog.Models;
using MyBlog.DAL.Context;

namespace MyBlog.DAL.Repository
{
    public class CommentRepository
    {
        private BlogContext _dbContext;

        public CommentRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<Comment> Get(bool includeData = true)
        {


            if (includeData)
            {
                return _dbContext.Comments.Include(c => c.Autor).Include(c => c.Post).Include(c => c.Post).Where<Comment>(c => c.IsHidden == false).ToList<Comment>();
            }

            return _dbContext.Comments.Include(c => c.Autor).Include(c => c.Post).Where<Comment>(c => c.IsHidden == false).ToList<Comment>();

        }

        public Comment Get(int id, bool includeData = true)
        {

            if (includeData)
            {
                _dbContext.Comments.Include(c => c.Autor).Include(c => c.Post).FirstOrDefault<Comment>(x => x.Id == id);
            }

            return _dbContext.Comments.FirstOrDefault<Comment>(x => x.Id == id);
        }

        public void Put(Comment item)
        {
            _dbContext.Comments.Add(item);
            _dbContext.SaveChanges();
        }



        public void DeleteById(int id)
        {

            Comment comment = _dbContext.Comments.FirstOrDefault(x => x.Id == id);

            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();
            }

        }


        public void Update(Comment item, bool isHiddel = false)
        {

            Comment comment = _dbContext.Comments.FirstOrDefault<Comment>(x => x.Id == item.Id);

            if (comment != null)
            {
                comment.IsHidden = isHiddel;
                comment.Content = item.Content;
                _dbContext.SaveChanges();
            }
        }
    }
}