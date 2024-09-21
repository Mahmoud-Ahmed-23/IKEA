using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employee
{
    public class CreatedEmployeeDto
    {
        [MaxLength(50, ErrorMessage = "max lenght of name is 50 chars")]
        [MinLength(5, ErrorMessage = "min lenght of name is 5 chars")]

        public string Name { get; set; } = null!;
        [Range(22, 30)]
        public int? Age { get; set; }

        public string? Adress { get; set; }
        //[DataType(DataType.Currency)]
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
        [Display(Name = "Emploee Type")]
        public EmployeeType EmployeeType { get; set; }
    }

}
