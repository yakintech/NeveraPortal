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
            List<IndexAdminUserVM> model = new List<IndexAdminUserVM>();
            model = _adminUserRepository.GetAll().Select(x => new IndexAdminUserVM
            {
                Id = x.Id,
                EMail = x.EMail,
                AddDate = x.AddDate,
                UpdateDate = x.UpdateDate,

            }).ToList();
            return View(model);
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
                return Redirect("~/admin/adminuser/");
            }
            else
            {
                return View(model);
            }
            
        }

        public IActionResult Edit(int id)
        {
            AdminUser user = _adminUserRepository.GetById(id);

            if (user == null)
            {
                return NotFound(); 
            }

            EditAdminUserVM model = new EditAdminUserVM
            {
                Id = user.Id,
                EMail = user.EMail,
     
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditAdminUserVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AdminUser userToUpdate = _adminUserRepository.GetById(model.Id);

                    if (userToUpdate == null)
                    {
                        return NotFound(); 
                    }

                    userToUpdate.EMail = model.EMail;

                    _adminUserRepository.Update(userToUpdate);

                    return Redirect("~/admin/adminuser/");
                }
                catch (Exception ex)
                {
                    return View("Error"); 
                }
            }
            else
            {
                return View(model);
            }
        }



        public IActionResult Delete(int id)
		{

			AdminUser user = _adminUserRepository.GetById(id);

			if (user == null)
			{
				return NotFound(); 
			}


			_adminUserRepository.Delete(id);

			return Redirect("~/admin/adminuser/");
		}



	}
}
