using LinkDev.IKEA.BLL.Common.Attchments;
using LinkDev.IKEA.BLL.Models.Employee;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using LinkDev.IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BLL.Services.Employees
{
	public class EmployeeService : IEmployeeService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IAttachmentService _attachmentService;

		public EmployeeService(IUnitOfWork unitOfWork, IAttachmentService attachmentService)
		{
			_unitOfWork = unitOfWork;
			_attachmentService = attachmentService;
		}


		public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
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
				DepartmentId = employeeDto.DepartmentId,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,

			};

			if (employeeDto.Image != null)
				employee.Image = _attachmentService.Upload(employeeDto.Image, "images");


			await _unitOfWork.EmployeeRepositry.AddAsync(employee);

			return await _unitOfWork.CompleteAsync();
		}


		public async Task<bool> DeleteEmployeeAsync(int id)
		{
			var employee = await _unitOfWork.EmployeeRepositry.GetAsync(id);
			if (employee is { })
			{
				_unitOfWork.EmployeeRepositry.Delete(employee);
			}

			return await _unitOfWork.CompleteAsync() > 0;
		}


		public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync(string search)
		{
			return await _unitOfWork.EmployeeRepositry.GetAllIQueryable().Where(e => !e.IsDeleted && (string.IsNullOrEmpty(search) || e.Name.ToLower().Contains(search.ToLower()))).Select(employee => new EmployeeToReturnDto
			{
				Id = employee.Id,
				Name = employee.Name,
				Age = employee.Age,
				IsActive = employee.IsActive,
				Salary = employee.Salary,
				EmailAddress = employee.EmailAddress,
				Gender = employee.Gender.ToString(),
				EmployeeType = employee.EmployeeType.ToString(),
				Department = employee.Department.Name,
			}).ToListAsync();
		}


		public async Task<EmployeeDetailsToReturnDto?> GetEmployeesByIdAsync(int id)
		{
			var employee = await _unitOfWork.EmployeeRepositry.GetAsync(id); ;
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
					Image = employee.Image,
					DepartmentId = employee.DepartmentId,
				};
			}
			else
				return null;
		}


		public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
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
				DepartmentId = employeeDto.DepartmentId,
				Image = employeeDto.Image,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
			};

			_unitOfWork.EmployeeRepositry.Update(employee);

			return await _unitOfWork.CompleteAsync();
		}


	}
}
