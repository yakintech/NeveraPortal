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
using NeveraPortal.UI.Areas.Admin.Models.AdminUser;
using NeveraPortal.UI.Areas.Admin.Models.Blog;
using NeveraPortal.UI.Areas.Admin.Models.Country;

namespace NeveraPortal.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IActionResult Index()
        {
            var countries = _countryRepository.GetAll();
            return View(countries);
        }

		public IActionResult Create()
		{
			return View();
		}

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateCountryVM model)
        {
            if (ModelState.IsValid)
            {
                Country country = new Country();
                country.Name = model.Name;
                

                _countryRepository.Create(country);
                return Redirect("~/admin/country/");
            }
            else
            {
                return View(model);
            }

        }
		public async Task<IActionResult> Delete(int id)
		{

			
			var result = _countryRepository.Delete(id);
			if (result == null)
			{
				return NotFound();
			}
			return Redirect("/Admin/Country/index");
		}

		public IActionResult Edit(int id)
		{
			var country = _countryRepository.GetById(id);
			if (country == null)
			{
				ViewBag.Error = "Something went wrong and country couldnt found";
				return View();
			}
			var model = new EditCountryVM
			{
				Id = country.Id,
				Name = country.Name,
			};
			return View(model);
		}
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCountryVM model)
        {
            if (ModelState.IsValid)
            {
                Country country = _countryRepository.GetById(model.Id);

                if (country == null)
                {
                    ViewBag.Error = "Something went wrong and country could not be found.";
                    return View(model);
                }

                country.Name = model.Name;

                _countryRepository.Update(country);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }





    }
}