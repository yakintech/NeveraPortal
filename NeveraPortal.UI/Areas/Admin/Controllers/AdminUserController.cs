using Microsoft.AspNetCore.Mvc;
using NeveraPortal.BLL.Services.Repositories;
using NeveraPortal.DAL.Models;
using NeveraPortal.UI.Areas.Admin.Models.AdminUser;

namespace NeveraPortal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        GenericRepository<AdminUser> _adminUserRepository;
        public AdminUserController() {

            _adminUserRepository = new GenericRepository<AdminUser>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateAdminUserVM model)
        {
            if (ModelState.IsValid)
            {
                AdminUser adminUser = new AdminUser();
                adminUser.EMail = model.EMail;
                adminUser.Password = model.Password;

                _adminUserRepository.Create(adminUser);
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
