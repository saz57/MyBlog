using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using MyBlog.Models;
using MyBlog.Enums;

namespace MyBlog.Repository
{
    public class UsersRepository
    {
        public List<ApplicationUser> Get(int pageNumber, int pageSize, bool includeData = true)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            using (BlogContext context = new BlogContext())
            {

                if (includeData)
                {
                    if (context.UsersSet.Count<ApplicationUser>() >= pageNumber * pageSize)
                    {
                        users = context.UsersSet.Include(u => u.Posts).Include(u => u.Comments).ToList<ApplicationUser>().GetRange((pageNumber - 1) * pageSize, pageSize);
                    }

                    if (context.UsersSet.Count<ApplicationUser>() < pageNumber * pageSize)
                    {
                        users = context.UsersSet.Include(u => u.Posts).Include(u => u.Comments).ToList<ApplicationUser>().GetRange((pageNumber - 1) * pageSize, context.UsersSet.Count<ApplicationUser>());
                    }

                }

                if (!includeData)
                {
                    if (context.UsersSet.Count<ApplicationUser>() >= pageNumber * pageSize)
                    {
                        users = context.UsersSet.ToList<ApplicationUser>().GetRange((pageNumber - 1) * pageSize, pageSize);
                    }

                    if (context.UsersSet.Count<ApplicationUser>() < pageNumber * pageSize)
                    {
                        users = context.UsersSet.ToList<ApplicationUser>().GetRange((pageNumber - 1) * pageSize, context.UsersSet.Count<ApplicationUser>());
                    }

                }
            }
            return users;


        }

        public ApplicationUser GetById(int id, bool includeData = true)
        {
            using (BlogContext context = new BlogContext())
            {
                if(includeData)
                {
                    return context.UsersSet.Include(u => u.Posts).Include(u => u.Comments).FirstOrDefault<ApplicationUser>(x => x.Id == id);
                }

                return  context.UsersSet.FirstOrDefault<ApplicationUser>(x => x.Id == id); 
            }
        }

        public ApplicationUser GetByLogin(string login, bool includeData = true)
        {
            
            using (BlogContext context = new BlogContext())
            {

                if (includeData)
                {
                    context.UsersSet.Include(u => u.Posts).Include(u => u.Comments).FirstOrDefault<ApplicationUser>(x => x.Login == login);
                }

                return context.UsersSet.FirstOrDefault<ApplicationUser>(x => x.Login == login);
            }
            
        }

        public ApplicationUser GetByNickName(string nickname, bool includeData = true)
        {
            using (BlogContext context = new BlogContext())
            {

                if (includeData)
                {
                    context.UsersSet.Include(u => u.Posts).Include(u => u.Comments).FirstOrDefault<ApplicationUser>(x => x.NickName == nickname);
                }

                return context.UsersSet.FirstOrDefault<ApplicationUser>(x => x.NickName == nickname);
            }
        }

        public void Put(ApplicationUser item)
        {
            using (BlogContext context = new BlogContext())
            {
                item.Role = UserRole.User;
                context.UsersSet.Add(item);
                context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            using (BlogContext context = new BlogContext())
            {
                context.UsersSet.Remove(context.UsersSet.FirstOrDefault(x => x.Id == id));
                context.SaveChanges();
            }
        }

        public void Update(ApplicationUser item, bool changeRole = false)
        {
            using (BlogContext context = new BlogContext())
            {
                ApplicationUser user = context.UsersSet.FirstOrDefault<ApplicationUser>(x => x.Id == item.Id);

                if (user != null)
                {
                    item.Posts = user.Posts;
                    item.Comments = user.Comments;

                    if (changeRole)
                    {
                        if(user.Role == UserRole.User)
                        {
                            item.Role = UserRole.Admin;
                        }

                        if (user.Role == UserRole.Admin)
                        {
                            item.Role = UserRole.User;
                        }
                    }

                    user = item;
                    context.SaveChanges();
                }

            }
        }
    }
}