using FirebaseTest.Bussiness;
using Microsoft.AspNetCore.Mvc;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.AdminUser;

namespace NeveraPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly FirebaseStorageHelper _firebaseStorageHelper;

        public BlogController()
        {
            _blogRepository = new BlogRepository();
            _firebaseStorageHelper = new FirebaseStorageHelper();
        }

        public IActionResult Index()
        {
            var blogs = _blogRepository.GetAll();
            return View(blogs);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateBlogVM model)
        {
            if (ModelState.IsValid)
            {
                var fs = model.MainImage.OpenReadStream();
                var imageUrl = _firebaseStorageHelper.AddImage(fs, model.Title).Result;
                fs.Close();

                Blog blog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    MainImgPath = imageUrl,
                    Tags = model.Tags,
                    AddDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                var result = _blogRepository.Create(blog);
                if (result == null)
                {
                    ViewBag.Error = "Something went wrong and blog couldnt added";
                    return View(model);
                }
                ViewBag.Result = "Blog Added Successfully";
                return View(model);
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Edit(int id)
        {
            TempData["Message"] = "Update method will be added soon";
            return Redirect("/Admin/Blog/index");
            //return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditAdminUserVM model)
        {

            TempData["Message"] = "Update method will be added soon";
            return Redirect("/Admin/Blog/index");

            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Delete(int id, string blogTitle)
        {

            _firebaseStorageHelper.DeleteImage(blogTitle);
            var result = _blogRepository.Delete(id);
            if (result == null)
            {
                ViewBag.Error = "Something went wrong and blog couldnt deleted";
                return Redirect("/Admin/Blog/index");
            }
            return Redirect("/Admin/Blog/index");
        }
    }
}
