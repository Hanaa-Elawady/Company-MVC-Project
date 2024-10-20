using Company.service.DTOs;
using Company.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company2.Web.Controllers
{
    [Authorize]
	public class DepartmentController : Controller
    {
        #region Dependancy injection

        private readonly IDepartmentService _departmentService;
        public DepartmentController( IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        #endregion

        #region View All 
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }
		#endregion

		#region Create 
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
		public IActionResult Create(DepartmentDto input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(input);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Department Error", "Invalid Name or code");
                return View(input);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(input);
            }

		}

        #endregion

        #region Update
            public IActionResult update(int id )
            {
                var department = _departmentService.GetById(id);
                if (department != null)
                   return View(department);
                
                return RedirectToAction("NotFoundPage", null, "Home");
            }

            [HttpPost]
            public IActionResult Update(DepartmentDto input)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        _departmentService.Update(input);
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("Department Error", "Invalid Name or code");
                    return View(input);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("DepartmentError", ex.Message);
                    return View(input);
                }
            }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            var department = _departmentService.GetByIdWithNoTracking(id);
            if (department != null)
                return View(department);

            return RedirectToAction("NotFoundPage", null, "Home");
        }
        #endregion

        #region Delete 
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetByIdWithNoTracking(id);

            if (department != null)
            {
                _departmentService.Delete(department);
                return RedirectToAction(nameof(Index));
            }

                return RedirectToAction("NotFoundPage", null, "Home");

        }
        #endregion
    }
}
