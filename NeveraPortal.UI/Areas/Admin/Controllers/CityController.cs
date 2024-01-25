using FirebaseTest.Bussiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.City;
using NeveraPortal.UI.Areas.Admin.Models.Country;

namespace NeveraPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public CityController(ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public IActionResult Index()
        {
            var city = _cityRepository.GetAll();
            return View(city);
        }
		public IActionResult Create()
		{
			var countries = _countryRepository.GetAll()
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.ToList();

			var model = new CreateCityVM { Countries = countries };
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Create(CreateCityVM model)
		{
			var countries = _countryRepository.GetAll()
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.ToList();

			model.Countries = countries;

			if (ModelState.IsValid)
			{
				try
				{
					City city = new City
					{
						Name = model.Name,
						CountryId = model.CountryId
					};

					_cityRepository.Create(city);

					return Redirect("~/admin/city/");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(nameof(CreateCityVM), "City creation failed. Please try again.");
					return View(model);
				}
			}
			else
			{
				return View(model);
			}
		}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var city = _cityRepository.GetById(id);

            if (city == null)
            {
                return NotFound();
            }

            var countries = _countryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            var model = new EditCityVM
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId ?? 0, // Use 0 if CountryId is null
                Countries = countries
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCityVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    City city = new City
                    {
                        Id = model.Id,
                        Name = model.Name,
                        CountryId = model.CountryId
                    };

                    _cityRepository.Update(city);

                    return Redirect("~/admin/city/");
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    ModelState.AddModelError(nameof(EditCityVM), "City edit failed. Please try again.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Delete(int id)
        {

            City city = _cityRepository.GetById(id);

            if (city == null)
            {
                return NotFound();
            }


            _cityRepository.Delete(id);

            return Redirect("~/admin/city/");
        }



    }
}
