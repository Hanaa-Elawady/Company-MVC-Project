using Company.Data.Models;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company2.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {

        #region dependancy injection

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        #endregion

        #region get all Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleModel)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    
                    Name = roleModel.Name,
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var item in result.Errors)
                    _logger.LogError(item.Description);
            }

            return View(roleModel);
        }
		#endregion

		#region Display
		public async Task<IActionResult> Details(string id)
        {
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
                return RedirectToAction("NotFoundPage", null, "Home");

            var roleViewModel = new RoleViewModel
            {
                Id = Role.Id,
                Name = Role.Name
            };

            return View(roleViewModel);

        }

        #endregion

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
                return RedirectToAction("NotFoundPage", null, "Home");

            var roleViewModel = new RoleViewModel
            {
                Id = Role.Id,
                Name = Role.Name
            };
            return View(roleViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, RoleViewModel roleModel)
        {
            if (roleModel.Id != id)
                return RedirectToAction("NotFoundPage", null, "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);

                    if (role == null)
                        return RedirectToAction("NotFoundPage", null, "Home");

                    role.Name = roleModel.Name;
                    role.NormalizedName = roleModel.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("role Updated Successfully");
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

            return View(roleModel);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));


                foreach (var item in result.Errors)
                    _logger.LogError(item.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }

            return RedirectToAction(nameof(Index));

        }
        #endregion        

        #region Add Or remove Users Rols
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();

            ViewBag.RoleId = roleId;
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserInroleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserInroleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInroleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser != null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);

                    }
                }
                return RedirectToAction("Update", new { id = roleId });
            }
            return View(users);
        }

        #endregion
    }

}
