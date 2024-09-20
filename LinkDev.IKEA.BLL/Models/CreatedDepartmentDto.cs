﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models
{
	public class CreatedDepartmentDto
	{
		public string Code { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string? Description { get; set; } = null!;
		public DateTime CreationDate { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreateOn { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime LastModifiedOn { get; set; }

	}
}
