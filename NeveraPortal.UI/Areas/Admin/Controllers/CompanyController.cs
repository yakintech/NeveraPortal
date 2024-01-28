using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.City;
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
            var companies = _companyRepository.GetCompanies();
            //.Select(c => new IndexCompanyVM
            // {
            //     Name = c.Name,
            //     Logo = c.Logo,
            //     Address = c.Address,
            //     CountryName = c.Country != null ? c.Country.Name : string.Empty
            // }).ToList();
            return View(companies);
        }

        public IActionResult Create()
        {
            var countries = _countryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            var model = new CreateCompanyVM { Countries = countries };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateCompanyVM model)
        {
            var countries = _countryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            model.Countries = countries;

            if (ModelState.IsValid)
            {
                try
                {
                    Company company = new Company
                    {
                        Name = model.Name,
                        Logo = model.Logo,
                        Address = model.Address,
                        CountryId = model.CountryId
                    };

                    _companyRepository.Create(company);

                    return Redirect("~/admin/company/");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(nameof(CreateCompanyVM), "Company creation failed. Please try again.");
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
            var companies = _companyRepository.GetById(id);
            if (companies == null)
            {
                return NotFound();
            }
            var model = new EditCompanyVM
            {
                Id = companies.Id,
                Name = companies.Name,
                Logo = companies.Logo,
                Address = companies.Address,
                CountryId = companies.CountryId,
                Countries = GetCountryList()
            };
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCompanyVM model)
        {

            if (ModelState.IsValid)
            {
                try
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

                    return RedirectToAction("Index", "Company");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            else
            {
                model.Countries = GetCountryList();
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
        //private List<SelectListItem> GetCityList()
        //{
        //    var cities = _cityRepository.GetAll()
        //        .Select(city => new SelectListItem
        //        {
        //            Value = city.Id.ToString(),
        //            Text = city.Name
        //        })
        //        .ToList();

        //    return cities;
        //}
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
