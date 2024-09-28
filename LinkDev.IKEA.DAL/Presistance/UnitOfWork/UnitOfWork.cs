using LinkDev.IKEA.DAL.Presistance.Data;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Departments;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.UnitOfWork
{
	internal class UnitOfWork : IUnitOfWork
	{

		private readonly ApplicationDbContext _dbContext;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEmployeeRepositry EmployeeRepositry => new EmployeeRepositry(_dbContext);
		public IDepartmentRepositry DepartmentRepositry => new DepartmentRepositry(_dbContext);

		public int Complete()
		{
			return _dbContext.SaveChanges();
		}
		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
