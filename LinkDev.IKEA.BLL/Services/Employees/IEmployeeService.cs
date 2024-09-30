using LinkDev.IKEA.BLL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync(string search);

        Task<EmployeeDetailsToReturnDto?> GetEmployeesByIdAsync(int id);

		Task<int> CreateEmployeeAsync(CreatedEmployeeDto departmentDto);

		Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto DepartmentDto);

		Task<bool> DeleteEmployeeAsync(int id);
    }
}
