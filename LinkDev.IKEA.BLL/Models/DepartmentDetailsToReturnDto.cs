using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models
{
	public class DepartmentDetailsToReturnDto
	{
		public int Id { get; set; }
		public string Code { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime CreationDate { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime LastModifiedOn { get; set; }
	}
}
