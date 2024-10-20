using Company.Data.Models;
using Company2.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company2.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
		#region dependancy injection

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<UserController> _logger;
		public UserController(UserManager<ApplicationUser> userManager , ILogger<UserController> logger)
		{
			_userManager = userManager;
			_logger = logger;
		}

		#endregion

		#region get all users
		public async Task<IActionResult> Index(string searchInput)
        {
			IEnumerable<ApplicationUser> Users;

			if (string.IsNullOrEmpty(searchInput))
				Users = await _userManager.Users.ToListAsync();
			else
				Users = await _userManager.Users.Where(user => user.NormalizedEmail.Trim().Contains(searchInput.Trim().ToUpper())).ToListAsync();

			return View(Users);
        }
		#endregion

		#region Display
		public async Task<IActionResult> Details(string id)
		{
			var User =await _userManager.FindByIdAsync(id);
			if (User == null)
				return RedirectToAction("NotFoundPage", null, "Home");

			return View(User);

		}

        #endregion

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return RedirectToAction("NotFoundPage", null, "Home");

            return View(User);

        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateViewModel applicationUser)
        {
            if (applicationUser.Id != id)
                return RedirectToAction("NotFoundPage", null, "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);

                    if (user == null)
                        return RedirectToAction("NotFoundPage", null, "Home");

                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully");
                        return RedirectToAction("Index");

                    }

                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(applicationUser);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return RedirectToAction("NotFoundPage", null, "Home");

            await _userManager.DeleteAsync(User);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
