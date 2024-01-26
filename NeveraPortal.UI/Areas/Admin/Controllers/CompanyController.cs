using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.Company;
using NeveraPortal.UI.Areas.Admin.Models.Country;

namespace NeveraPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;

        public CompanyController(ICompanyRepository companyRepository, ICountryRepository countryRepository, ICityRepository cityRepository)
        {
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        public IActionResult Index()
        {
            var companies = _companyRepository.GetAll();
            return View(companies);
        }

        public IActionResult Edit(int id)
        {
            var companies = _companyRepository.GetById(id);
            if(companies == null)
            {
                return NotFound();
            }
            var model = new EditCompanyVM
            {
                Name = companies.Name,
                Logo = companies.Logo,
                Address = companies.Address,
                CountryId = companies.CountryId,
                Countries = GetCountryList(),
                Cities = GetCityList()
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCompanyVM model)
        {
            if (ModelState.IsValid)
            {
                Company company = _companyRepository.GetById(model.Id);

                if (company == null)
                {
                    return NotFound();
                }

                company.Name = model.Name;
                company.Logo = model.Logo;
                company.Address = model.Address;
                company.CountryId = model.CountryId;

                _companyRepository.Update(company);

                return RedirectToAction("Index","Company");
            }
            else
            {
                model.Countries = GetCountryList();
                model.Cities = GetCityList();
                return View(model);
            }
        }
        private List<SelectListItem> GetCountryList()
        {
            var countries = _countryRepository.GetAll()
                 .Select(c => new SelectListItem
                 {
                     Value = c.Id.ToString(),
                     Text = c.Name
                 })
                 .ToList();

            return countries;
        }
        private List<SelectListItem> GetCityList()
        {
            var cities = _cityRepository.GetAll()
                .Select(city => new SelectListItem
                {
                    Value = city.Id.ToString(),
                    Text = city.Name
                })
                .ToList();

            return cities;
        }
		public IActionResult Delete(int id)
		{
			var model = _companyRepository.Delete(id);
			if (model == null)
			{
				return NotFound();
			}
            return RedirectToAction("Index", "Company");
		}
	}
}
