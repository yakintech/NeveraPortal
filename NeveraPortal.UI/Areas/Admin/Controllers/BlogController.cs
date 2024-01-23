using FirebaseTest.Bussiness;
using Microsoft.AspNetCore.Mvc;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.AdminUser;
using NeveraPortal.UI.Areas.Admin.Models.Blog;

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
            if (model.Tags == null)
                model.Tags = string.Empty;



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
                    ViewBag.Error = "Something went wrong and blog couldn't added";
                    return View(model);
                }
                TempData["Message"] = "Blog Added Successfully";
                return Redirect("/Admin/Blog/index");
            }

            return View(model);

        }

        public IActionResult Edit(int id)
        {
            var blog = _blogRepository.GetById(id);
            if (blog == null)
            {
                ViewBag.Error = "Something went wrong and blog couldnt found";
                return View();
            }
            var model = new EditBlogVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Tags = blog.Tags,
                MainImgPath = blog.MainImgPath
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditBlogVM model)
        {
            if (model.Tags == null)
            {
                model.Tags = string.Empty;
            }

            if (ModelState.IsValid)
            {
                if (model.MainImage != null)
                {
                    var fs = model.MainImage.OpenReadStream();
                    var imageUrl = _firebaseStorageHelper.AddImage(fs, model.Title).Result;
                    fs.Close();
                    model.MainImgPath = imageUrl;
                }
                var blogFromDB = _blogRepository.GetById(model.Id);
                blogFromDB.UpdateDate = DateTime.Now;
                blogFromDB.Title = model.Title;
                blogFromDB.Content = model.Content;
                blogFromDB.Tags = model.Tags;
                blogFromDB.MainImgPath = model.MainImgPath;
                _blogRepository.Update(blogFromDB);

                TempData["Message"] = "Blog Updated Successfully";
                return Redirect("/Admin/Blog/index");
            }

            return View(model);

        }

        public async Task<IActionResult> Delete(int id, string blogTitle)
        {

            await _firebaseStorageHelper.DeleteImage(blogTitle);
            var result = _blogRepository.Delete(id);
            if (result == null)
            {
                TempData["Message"] = "Something went wrong and blog couldnt deleted";
            }
            return Redirect("/Admin/Blog/index");
        }
    }
}
