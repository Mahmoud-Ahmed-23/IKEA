using AutoMapper;
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
		private readonly IMapper _mapper;

		public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment, IMapper mapper)
		{
			_departmentService = departmentService;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
		}

		#endregion


		#region Index

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var deparments = await _departmentService.GetAllDepartmentsAsync();
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
		public async Task<IActionResult> Create(DepartmentViewModel department)
		{
			if (!ModelState.IsValid)
				return View(department);
			var message = string.Empty;
			try
			{

				//var result = _departmentService.CreateDepartment(department);

				var createdDepartment = _mapper.Map<CreatedDepartmentDto>(department);
				var result = await _departmentService.CreateDepartmentAsync(createdDepartment);

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
		public async Task<IActionResult> Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

			if (department is null)
				return NotFound();

			return View(department);
		}

		#endregion


		#region Edit

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

			if (department is null)
				return NotFound();

			var departmentVM = _mapper.Map<DepartmentViewModel>(department);

			return View(departmentVM);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Edit(int id, DepartmentViewModel departmentVM)
		{
			if (!ModelState.IsValid)
				return View(departmentVM);

			string message = string.Empty;

			try
			{

				var UpdatedDepartment = _mapper.Map<UpdatedDepartmentDto>(departmentVM);

				var Updated = await _departmentService.UpdateDepartment(UpdatedDepartment) > 0;

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
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

			if (department is null)
				return NotFound();

			return View(department);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var message = string.Empty;

			try
			{

				var deleted = await _departmentService.DeleteDepartmentAsync(id);

				if (deleted)
				{
					TempData["Deleted"] = $"Department {_departmentService.GetDepartmentByIdAsync(id).Result?.Name} is Deleted";
				}
				else
					TempData["Deleted"] = $"Department {_departmentService.GetDepartmentByIdAsync(id).Result?.Name} is Not Deleted";


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
