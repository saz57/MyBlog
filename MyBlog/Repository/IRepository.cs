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
        List<T> Get<T>(int pageNumber, int pageSize) where T : Post;
        T Get<T>(int Id) where T : Post;
        void Put<T>(T item) where T : Post;
        void DeleteById(int Id); //where T : Post;
        void Update<T>(T item) where T : Post;
    }
}
