using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
	public class DepartmentEditViewModel
	{
		public string Code { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string? Description { get; set; }

		[Display(Name = "Creation Date")]
		public DateTime CreationDate { get; set; }
	}
}
