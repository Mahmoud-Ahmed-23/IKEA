using LinkDev.IKEA.BLL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeesService
    {
        IEnumerable<EmployeeToReturnDto> GetAllEmployees();

        EmployeeDetailsToReturnDto? GetEmployeesById(int id);

        int CreateEmployee(CreatedEmployeeDto departmentDto);

        int UpdateEmployee(UpdatedEmployeeDto DepartmentDto);

        bool DeleteEmployee(int id);
    }
}
