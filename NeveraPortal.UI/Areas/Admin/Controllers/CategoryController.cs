using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FirebaseTest.Bussiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.Category;


namespace NeveraPortal.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Name = model.Name;
                category.Description = model.Description;

                _categoryRepository.Create(category);
                return Redirect("~/admin/category/");
            }
            else
            {
                return View(model);
            }

        }
        public async Task<IActionResult> Delete(int id)
        {


            var result = _categoryRepository.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Redirect("/Admin/Category/index");
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                ViewBag.Error = "Something went wrong and category couldnt found";
                return View();
            }
            var model = new EditCategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                Category category = _categoryRepository.GetById(model.Id);

                if (category == null)
                {
                    ViewBag.Error = "Something went wrong and category could not be found.";
                    return View(model);
                }

                category.Name = model.Name;
                category.Description = model.Description;
                _categoryRepository.Update(category);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }





    }
}