using LinkDev.IKEA.BLL.Models.Department;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Departments;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using LinkDev.IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BLL.Services.Departments
{
	public class DepartmentService : IDepartmentService
	{

		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}



		public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
		{
			var departments = await _unitOfWork.DepartmentRepositry.GetAllIQueryable().Where(d => !d.IsDeleted).Select(department => new DepartmentToReturnDto
			{
				Id = department.Id,
				Name = department.Name,
				Code = department.Code,
				Description = department.Description,
				CreationDate = department.CreationDate,
			}).ToListAsync();

			return departments;

		}

		public async Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id)
		{
			var department = await _unitOfWork.DepartmentRepositry.GetAsync(id);
			if (department is { })
				return new DepartmentDetailsToReturnDto
				{
					Id = department.Id,
					Name = department.Name,
					Code = department.Code,
					Description = department.Description,
					CreationDate = department.CreationDate,
					CreatedBy = department.CreatedBy,
					CreatedOn = department.CreatedOn,
					LastModifiedBy = department.LastModifiedBy,
					LastModifiedOn = department.LastModifiedOn,
				};
			else
				return null;
		}

		public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto DepartmentDto)
		{
			var department = new Department
			{
				Code = DepartmentDto.Code,
				Name = DepartmentDto.Name,
				Description = DepartmentDto.Description,
				CreationDate = DepartmentDto.CreationDate,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DepartmentDto.LastModifiedOn,
			};
			await _unitOfWork.DepartmentRepositry.AddAsync(department);

			return await _unitOfWork.CompleteAsync();

		}

		public async Task<int> UpdateDepartment(UpdatedDepartmentDto DepartmentDto)
		{
			var department = new Department
			{
				Id = DepartmentDto.Id,
				Code = DepartmentDto.Code,
				Name = DepartmentDto.Name,
				Description = DepartmentDto.Description,
				CreationDate = DepartmentDto.CreationDate,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DepartmentDto.LastModifiedOn,
			};
			_unitOfWork.DepartmentRepositry.Update(department);
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<bool> DeleteDepartmentAsync(int id)
		{
			var department = await _unitOfWork.DepartmentRepositry.GetAsync(id);

			if (department is { })
			{
				var employees = _unitOfWork.EmployeeRepositry.GetAllIQueryable().Where(e => e.Department == department);

				foreach (var employee in employees)
				{
					employee.DepartmentId = null;
				}


				_unitOfWork.DepartmentRepositry.Delete(department);

			}

			return await _unitOfWork.CompleteAsync() > 0;
		}


	}
}