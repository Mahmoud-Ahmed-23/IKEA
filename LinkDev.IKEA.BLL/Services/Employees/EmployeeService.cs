using LinkDev.IKEA.BLL.Models.Employee;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;

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

			var employee = new Employee()
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
				LastModifiedOn = DateTime.UtcNow

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


		public IEnumerable<EmployeeToReturnDto> GetEmployees(string search)
		{
			return _employeeRepository.GetAll().Where(e => !e.IsDeleted && (string.IsNullOrEmpty(search) || e.Name.ToLower().Contains(search.ToLower()))).Select(employee => new EmployeeToReturnDto
			{
				Id = employee.Id,
				Name = employee.Name,
				Age = employee.Age,
				IsActive = employee.IsActive,
				Salary = employee.Salary,
				EmailAddress = employee.EmailAddress,
				Gender = employee.Gender.ToString(),
				EmployeeType = employee.EmployeeType.ToString(),
			});
		}


		public EmployeeDetailsToReturnDto? GetEmployeesById(int id)
		{
			var employee = _employeeRepository.Get(id); ;
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

				};
			}
			else
				return null;
		}


		public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
		{
			var employee = new Employee()
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
				LastModifiedOn = DateTime.UtcNow

			};
			return _employeeRepository.Update(employee);
		}


	}
}
