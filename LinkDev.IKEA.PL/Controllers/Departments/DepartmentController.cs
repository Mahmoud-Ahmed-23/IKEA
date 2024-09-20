using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers.Department
{
	public class DepartmentController : Controller
	{

		private readonly IDepartmentService _departmentService;
		private readonly ILogger<DepartmentController> _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;


		public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment)
		{
			_departmentService = departmentService;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var deparments = _departmentService.GetAllDepartments();
			return View(deparments);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreatedDepartmentDto department)
		{
			if (!ModelState.IsValid)
				return View(department);
			var message = string.Empty;
			try
			{

				var result = _departmentService.CreateDepartment(department);

				if (result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					message = "Department is not Created";
					ModelState.AddModelError(string.Empty, message);
					return View(result);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				if (_webHostEnvironment.IsDevelopment())
				{
					message = ex.Message;
					return View(department);
				}
				else
				{
					message = "Department is not Created";
					return View("Error", message);
				}
			}
		}

		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = _departmentService.GetDepartmentById(id.Value);

			if (department is null)
				return NotFound();

			return View(department);
		}


		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = _departmentService.GetDepartmentById(id.Value);

			if (department is null)
				return NotFound();

			return View(new DepartmentEditViewModel()
			{
				Code = department.Code,
				Name = department.Name,
				Description = department.Description,
				CreationDate = department.CreationDate,
			});
		}


		[HttpPost]
		public IActionResult Edit(int id, DepartmentEditViewModel departmentVM)
		{
			if (!ModelState.IsValid)
				return View(departmentVM);

			string message = string.Empty;

			try
			{

				var UpdatedDepartment = new UpdatedDepartmentDto()
				{
					Id = id,
					Code = departmentVM.Code,
					Name = departmentVM.Name,
					Description = departmentVM.Description,
					CreationDate = departmentVM.CreationDate,
				};

				var Updated = _departmentService.UpdateDepartment(UpdatedDepartment) > 0;

				if (Updated)
					return RedirectToAction("Index");

				message = "an error has occured during updating the deparment :(";
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the deparment :(";

			}
			ModelState.AddModelError(string.Empty, message);
			return View(departmentVM);

		}

	

	}
}
