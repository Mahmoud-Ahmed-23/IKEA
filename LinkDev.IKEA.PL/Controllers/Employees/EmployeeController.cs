using AutoMapper;
using LinkDev.IKEA.BLL.Models.Department;
using LinkDev.IKEA.BLL.Models.Employee;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.PL.Controllers.Department;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers.Employees
{
	public class EmployeeController : Controller
	{
		#region Services



		private readonly IEmployeeService _employeeService;
		private readonly ILogger<DepartmentController> _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;

		public EmployeeController(IEmployeeService employeeService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment, IMapper mapper)
		{
			_employeeService = employeeService;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
		}

		#endregion


		#region Index

		[HttpGet]

		public IActionResult Index(string search)
		{
			var employees = _employeeService.GetEmployees(search);
			return View(employees);
		}


		#endregion


		#region Create

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Create(CreatedEmployeeDto employeeDto)
		{
			if (!ModelState.IsValid)
				return View(employeeDto);
			var message = string.Empty;
			try
			{
				var result = _employeeService.CreateEmployee(employeeDto);
				if (result > 0)
					TempData["Created"] = $"Employee {employeeDto.Name} is Created";
				else
					TempData["Created"] = $"Employee {employeeDto.Name} is Not Created";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				if (_webHostEnvironment.IsDevelopment())
				{
					message = ex.Message;
					return View(employeeDto);
				}
				else
				{
					message = "Employee is not Created";
					return View("Error", message);
				}
			}
		}

		#endregion


		#region Details

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var employee = _employeeService.GetEmployeesById(id.Value);
			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}

		#endregion


		#region Edit

		[HttpGet]
		public IActionResult Edit(int? id, [FromServices] IDepartmentService departmentService)
		{

			if (id is null)
			{
				return BadRequest();
			}

			var employee = _employeeService.GetEmployeesById(id.Value);

			// Check if department exists
			if (employee == null)
			{
				return NotFound();  // Return 404 if the department is not found
			}

			ViewData["Departments"] = departmentService.GetAllDepartments();

			var UpdatedEmployee = new UpdatedEmployeeDto
			{
				Id = employee.Id,
				Name = employee.Name,
				Address = employee.Address,
				Age = employee.Age,
				EmployeeType = employee.EmployeeType,
				Image = employee.Image,
				DepartmentId = employee.DepartmentId,
				EmailAddress = employee.EmailAddress,
				Gender = employee.Gender,
				HiringDate = employee.HiringDate,
				IsActive = employee.IsActive,
				PhoneNumber = employee.PhoneNumber,
				Salary = employee.Salary,
			};

			// Pass the fetched department data to the view
			return View(employee);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employeeVM)
		{
			if (!ModelState.IsValid)
			{
				return View(employeeVM);
			}
			var message = string.Empty;
			try
			{


				var updated = _employeeService.UpdateEmployee(employeeVM) > 0;

				if (updated)
					TempData["Updated"] = $"Employee {employeeVM.Name} is Updated";
				else
					TempData["Updated"] = $"Employee {employeeVM.Name} is Not Updated";

				message = "an error has occured during updating the employee";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the employee";

			}
			ModelState.AddModelError(string.Empty, message);
			return View(employeeVM);
		}

		#endregion


		#region Delete

		//[HttpGet]
		//public IActionResult Delete(int? id)
		//{
		//	if (id == null)
		//	{
		//		return BadRequest();
		//	}
		//	var department = _employeeService.GetEmployeesById(id.Value);

		//	if (department == null)
		//	{
		//		return NotFound();
		//	}
		//	return View(department);
		//}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var message = string.Empty;

			try
			{
				var deleted = _employeeService.DeleteEmployee(id);
				if (deleted)
					TempData["Deleted"] = $"Employee {_employeeService.GetEmployeesById(id)?.Name} is Deleted";
				else
					TempData["Deleted"] = $"Employee {_employeeService.GetEmployeesById(id)?.Name} is Not Deleted";


				message = "an error has occured during deleting the employee";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during deleting the employee :(";
			}
			//ModelState.AddModelError(string.Empty, message);

			// shoud use torser and use tempedata[]
			return RedirectToAction(nameof(Index));

		}

		#endregion
	}
}
