using LinkDev.IKEA.DAL.Entities.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Reposatories.Departments
{
	public interface IDepartmentRepositry
	{
		IEnumerable<Department> GetAll(bool AsNoTracking);
		Department? Get(int id);
		int Add(Department department);
		int Update(Department department);
		int Delete(Department department);

	}
}
