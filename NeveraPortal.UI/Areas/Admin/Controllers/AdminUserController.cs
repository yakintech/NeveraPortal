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
                return View();
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
                return NotFound(); // Or handle the case where the user is not found
            }

            EditAdminUserVM model = new EditAdminUserVM
            {
                Id = user.Id,
                EMail = user.EMail,
                // Set other properties as needed
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
                    // Fetch the user from the repository
                    AdminUser userToUpdate = _adminUserRepository.GetById(model.Id);

                    if (userToUpdate == null)
                    {
                        return NotFound(); // Or handle the case where the user is not found
                    }

                    // Update user properties with values from the view model
                    userToUpdate.EMail = model.EMail;
                    // Update other properties as needed

                    // Save changes to the repository
                    _adminUserRepository.Update(userToUpdate);

                    return RedirectToAction(nameof(Index), new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, display an error message, etc.)
                    return View("Error"); // You can create an error view to display a user-friendly error message
                }
            }
            else
            {
                return View(model);
            }
        }



        public IActionResult Delete(int id)
		{
			// Fetch the user details based on the provided id
			AdminUser user = _adminUserRepository.GetById(id);

			if (user == null)
			{
				return NotFound(); // Or handle the case where the user is not found
			}

			// You may want to show a confirmation page or directly delete the user
			_adminUserRepository.Delete(id);

			return RedirectToAction(nameof(Index), new { area = "Admin" }); // Specify the area
		}



	}
}
