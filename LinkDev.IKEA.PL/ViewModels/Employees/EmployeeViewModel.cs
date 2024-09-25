using LinkDev.IKEA.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
	public class EmployeeViewModel
	{


        [Required]
		[MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
		[MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
		public string Name { get; set; } = null!;

		[Range(20, 30)]
		public int? Age { get; set; }

		public string? Address { get; set; }

		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		[EmailAddress]
		public string? EmailAddress { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

		[Display(Name ="Deparment Name")]
        public string? Department { get; set; }

        public int? DepartmentId { get; set; }

    }
}
