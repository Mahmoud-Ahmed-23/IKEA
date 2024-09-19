using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers.Department
{
	public class DepartmentController : Controller
	{

		private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public DepartmentController(IDepartmentService departmentService, ILogger logger, IWebHostEnvironment webHostEnvironment)
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





    }
}
