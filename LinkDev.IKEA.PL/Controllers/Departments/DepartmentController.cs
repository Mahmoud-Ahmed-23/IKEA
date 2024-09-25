using LinkDev.IKEA.BLL.Models.Department;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LinkDev.IKEA.PL.Controllers.Department
{
	public class DepartmentController : Controller
	{
		#region Service

		private readonly IDepartmentService _departmentService;
		private readonly ILogger<DepartmentController> _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;


		public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment)
		{
			_departmentService = departmentService;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		#endregion


		#region Index

		[HttpGet]
		public IActionResult Index()
		{
			var deparments = _departmentService.GetAllDepartments();
			return View(deparments);
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
		public IActionResult Create(DepartmentViewModel department)
		{
			if (!ModelState.IsValid)
				return View(department);
			var message = string.Empty;
			try
			{

				//var result = _departmentService.CreateDepartment(department);

				var createdDepartment = new CreatedDepartmentDto
				{
					Code = department.Code,
					Name = department.Name,
					CreationDate = department.CreationDate,
					Description = department.Description
				};
				var result = _departmentService.CreateDepartment(createdDepartment);

				if (result > 0)
				{
					TempData["Created"] = $"Department {department.Name} is Created";
				}
				else
					TempData["Created"] = $"Department {department.Name} is not Created";


				return RedirectToAction(nameof(Index));

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

		#endregion


		#region Details

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

		#endregion


		#region Edit

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = _departmentService.GetDepartmentById(id.Value);

			if (department is null)
				return NotFound();

			return View(new DepartmentViewModel()
			{
				Code = department.Code,
				Name = department.Name,
				Description = department.Description,
				CreationDate = department.CreationDate,
			});
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Edit(int id, DepartmentViewModel departmentVM)
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
				{
					TempData["Updated"] = $"Department {departmentVM.Name} is Updated";
				}
				else
					TempData["Updated"] = $"Department {departmentVM.Name} is not Updated";

				message = "an error has occured during updating the deparment :(";

				return RedirectToAction("Index");

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the deparment :(";

			}
			ModelState.AddModelError(string.Empty, message);
			return View(departmentVM);

		}

		#endregion


		#region Delete

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = _departmentService.GetDepartmentById(id.Value);

			if (department is null)
				return NotFound();

			return View(department);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var message = string.Empty;

			try
			{

				var deleted = _departmentService.DeleteDepartment(id);

				if (deleted)
				{
					TempData["Deleted"] = $"Department {_departmentService.GetDepartmentById(id)?.Name} is Deleted";
				}
				else
					TempData["Deleted"] = $"Department {_departmentService.GetDepartmentById(id)?.Name} is Not Deleted";


				message = "an error has occured during deleting the deparment :(";
				return RedirectToAction(nameof(Index));

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during deleting the deparment :(";
			}

			//ModelState.AddModelError(string.Empty, message);

			return RedirectToAction(nameof(Index));

		}

		#endregion
	}
}
