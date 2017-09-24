using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using MyBlog.Models;
using MyBlog.ViewModels;
using MyBlog.DAL.Context;

namespace MyBlog.DAL.Repository
{
    public class PictureRepository
    {
        private BlogContext _dbContext;

        public PictureRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.Pictures.Count();
        }

        public List<Picture> Get()
        {
            return _dbContext.Pictures.ToList();
        }


        public Picture Get(int id)
        {
            Picture picture = _dbContext.Pictures.FirstOrDefault(p => p.Id == id);
            return picture;
        }

        public Picture Put(HttpPostedFileBase file, Post post)
        {
            if (file != null)
            {

                string fileName = String.Empty;
                int lastElementIndex = _dbContext.Pictures.Count() - 1;

                Picture lastPicture = _dbContext.Pictures.ToList().ElementAtOrDefault(lastElementIndex);

                if (lastPicture != null)
                {
                    int i = lastPicture.Id + 1;
                    fileName = i.ToString();
                }


                if (String.IsNullOrEmpty(fileName))
                {
                    fileName = "1";
                }

                fileName += Path.GetExtension(file.FileName);
                string path = "~/Content/Pictures/" + fileName;
                file.SaveAs(HttpContext.Current.Server.MapPath(path));

                Picture picture = new Picture() { PicturePath = path, MimeType = file.ContentType, Post = post };

                _dbContext.Pictures.Add(picture);
                _dbContext.SaveChanges();
                picture = _dbContext.Pictures.FirstOrDefault(p => p.PicturePath == picture.PicturePath && p.MimeType == picture.MimeType);

                if (picture != null)
                {
                    return picture;
                }
            }
            return null;
        }

        public void DeleteById(int id)
        {
            Picture picture = _dbContext.Pictures.FirstOrDefault(p=> p.Id == id);

            if (picture != null)
            {
                _dbContext.Pictures.Remove(picture);
                _dbContext.SaveChanges();
            }

        }
    }
}