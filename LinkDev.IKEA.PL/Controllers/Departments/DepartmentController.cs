using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers.Department
{
	public class DepartmentController : Controller
	{

		private readonly IDepartmentService _departmentService;

		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var deparments = _departmentService.GetAllDepartments();
			return View(deparments);
		}
	}
}
