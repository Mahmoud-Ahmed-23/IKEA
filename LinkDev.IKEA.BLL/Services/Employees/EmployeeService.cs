using LinkDev.IKEA.BLL.Models.Employee;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Migrations;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using Microsoft.EntityFrameworkCore;
namespace LinkDev.IKEA.BLL.Services.Employees
{
	public class EmployeeService : IEmployeeService
	{

		private readonly IEmployeeRepositry _employeeRepository;

		public EmployeeService(IEmployeeRepositry employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}


		public int CreateEmployee(CreatedEmployeeDto employeeDto)
		{

			var employee = new DAL.Entities.Employees.Employee()
			{
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Address = employeeDto.Address,
				IsActive = employeeDto.IsActive,
				Salary = employeeDto.Salary,
				EmailAddress = employeeDto.EmailAddress,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
				DepartmentId = employeeDto.DepartmentId,


			};

			return _employeeRepository.Add(employee);
		}


		public bool DeleteEmployee(int id)
		{
			var employee = _employeeRepository.Get(id);
			if (employee is { })
			{
				return _employeeRepository.Delete(employee) > 0;
			}

			return false;
		}


		public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
		{

			return _employeeRepository.GetAllIQueryable().Where(e => !e.IsDeleted).Include(e => e.Department).Select(employee => new EmployeeToReturnDto
			{
				Id = employee.Id,
				Name = employee.Name,
				Age = employee.Age,
				IsActive = employee.IsActive,
				Salary = employee.Salary,
				EmailAddress = employee.EmailAddress,
				Gender = employee.Gender.ToString(),
				EmployeeType = employee.EmployeeType.ToString(),
				Department = employee.Department.Name
			});
		}


		public EmployeeDetailsToReturnDto? GetEmployeesById(int id)
		{
			var employee = _employeeRepository.Get(id);
			if (employee is { })
			{
				return new EmployeeDetailsToReturnDto()
				{
					Name = employee.Name,
					Age = employee.Age,
					Address = employee.Address,
					IsActive = employee.IsActive,
					Salary = employee.Salary,
					EmailAddress = employee.EmailAddress,
					PhoneNumber = employee.PhoneNumber,
					HiringDate = employee.HiringDate,
					Gender = employee.Gender,
					EmployeeType = employee.EmployeeType,
					Department = employee.Department?.Name,

				};
			}
			else
				return null;
		}


		public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
		{
			var employee = new DAL.Entities.Employees.Employee()
			{
				Id = employeeDto.Id,
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Address = employeeDto.Address,
				IsActive = employeeDto.IsActive,
				Salary = employeeDto.Salary,
				EmailAddress = employeeDto.EmailAddress,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
				DepartmentId = employeeDto.DepartmentId,

			};
			return _employeeRepository.Update(employee);
		}


	}
}
