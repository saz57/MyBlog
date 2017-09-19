using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using MyBlog.DAL.RepositoriesManager;
using MyBlog.ViewModels;
using MyBlog.Models;
using PagedList;

namespace MyBlog.Controllers
{
    public class TestController : Controller
    {
        private List<Picture> _pictures;

        public TestController()
        {
            _pictures = new List<Picture>();
        }

        public ActionResult Index()
        {
            PostViewModel viewModel = new PostViewModel() { Id = 0, AutorId = "lol ceck cheburek", Content = "memasiki", Name = "birja memov", Images = new List<PictureViewModel>() };
            _pictures = RepositoryManager.PictureRepository.Get();

            foreach (Picture picture in _pictures)
            {
                viewModel.Images.Add(new PictureViewModel() {Id = picture.Id, MimeType = picture.MimeType, PicturePath = picture.PicturePath, PostId = 999 });
            }

            return View("Index", viewModel);
        }

        public ActionResult GetImages()
        {
            return PartialView("ImagePartical", RepositoryManager.PictureRepository.Get());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase data)
        {
            if (ModelState.IsValid)
            {
                if (data != null)
                {
                    //int id = RepositoryManager.PictureRepository.Put(data);
                    return Index();
                }
                
            }
            return Index();
        }

        [HttpPost]
        public JsonResult UploadAjax()
        {
            int counter = 0;
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    counter++;

                    HttpPostedFileBase upload = Request.Files[file];

                    
                    if (upload != null)
                    {
                        //int id = RepositoryManager.PictureRepository.Put(upload);
                        //_pictures.Add(RepositoryManager.PictureRepository.Get(id));

                    }
                }
                //return Json(_pictures.Count.ToString());

            }
            return Json("Failed");
        }
    }
}
