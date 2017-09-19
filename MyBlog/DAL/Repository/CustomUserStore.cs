﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MyBlog.Models;
using Microsoft.AspNet.Identity;

namespace MyBlog.Repository
{
    public class CustomUserStore : IUserStore<ApplicationUser>
    {
        static readonly List<ApplicationUser> Users = new List<ApplicationUser>();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => Users.Add(user));
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task<ApplicationUser>.Factory.StartNew(() => Users.FirstOrDefault(u => u.UserName == userName));
        }
    }
}