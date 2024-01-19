using Microsoft.AspNetCore.Mvc;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.AdminUser;

namespace NeveraPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private GenericRepository<Blog> _blogRepository;

        public BlogController()
        {
            _blogRepository = new GenericRepository<Blog>();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateBlogVM model)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    MainImgPath = model.MainImgPath,
                    Tags = model.Tags
                };

                _blogRepository.Create(blog);
                return View();
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditAdminUserVM model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
