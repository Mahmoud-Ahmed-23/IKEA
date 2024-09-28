using LinkDev.IKEA.DAL.Presistance.Reposatories.Departments;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IEmployeeRepositry EmployeeRepositry { get; }
		IDepartmentRepositry DepartmentRepositry { get; }
		int Complete();

	}
}
