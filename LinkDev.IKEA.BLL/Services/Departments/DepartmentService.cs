using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Departments;

namespace LinkDev.IKEA.BLL.Services.Departments
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IDepartmentRepositry _departmentRepositry;

		public DepartmentService(IDepartmentRepositry departmentRepositry)
		{
			_departmentRepositry = departmentRepositry;
		}

		public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
		{
			var departments = _departmentRepositry.GetAll();

			foreach (var department in departments)
			{
				yield return new DepartmentToReturnDto
				{
					Id = department.Id,
					Name = department.Name,
					Code = department.Code,
					Description = department.Description,
					CreationDate = department.CreationDate,
				};
			}

		}

		public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
		{
			var department = _departmentRepositry.Get(id);
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

		public int CreateDepartment(CreatedDepartmentDto DepartmentDto)
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
			return _departmentRepositry.Add(department);
		}

		public int UpdateDepartment(UpdatedDepartmentDto DepartmentDto)
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
			return _departmentRepositry.Update(department);
		}

		public bool DeleteDepartment(int id)
		{
			var department = _departmentRepositry.Get(id);

			if (department is { })
				return _departmentRepositry.Delete(department) > 0;
			else
				return false;
		}

	}
}