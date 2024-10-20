using Company.Data.Entities;
using Company.service.DTOs;
using Company.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company2.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Dependancy injection

        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService ,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        #endregion

        #region View All 
        public IActionResult Index(string searchInput )
        {
            IEnumerable<EmployeeDto> employees;
            if (String.IsNullOrEmpty(searchInput))
            {
                employees = _employeeService.GetAll();
            }
            else
            {
                employees = _employeeService.GetEmployeeByName(searchInput);

			}
                return View(employees);
        }
        #endregion

        #region Create 
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments =  _departmentService.GetAll().ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(input);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                    return View(input);
                }
            }
            catch (Exception ex)
            {
                return View(input);
            }

        }

        #endregion

        #region Update
        public IActionResult update(int id)
        {
            var Employee = _employeeService.GetById(id);
            if (Employee != null)
            {
                ViewBag.Departments = _departmentService.GetAll().ToList();
                return View(Employee);

            }

            return RedirectToAction("NotFoundPage", null, "Home");
        }

        [HttpPost]
        public IActionResult Update(EmployeeDto input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Update(input);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Employee Error", "Invalid data");
                return View(input);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Employee Errors", ex.Message);
                return View(input);
            }
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetByIdWithNoTracking(id);
            if (employee != null)
                return View(employee);

            return RedirectToAction("NotFoundPage", null, "Home");
        }
        #endregion

        #region Delete 
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetByIdWithNoTracking(id);

            if (employee != null)
            {
                _employeeService.Delete(employee);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("NotFoundPage", null, "Home");

        }
        #endregion
    }
}
