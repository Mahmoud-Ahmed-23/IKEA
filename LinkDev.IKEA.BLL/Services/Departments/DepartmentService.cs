using LinkDev.IKEA.BLL.Models.Department;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Departments;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using LinkDev.IKEA.DAL.Presistance.UnitOfWork;

namespace LinkDev.IKEA.BLL.Services.Departments
{
	public class DepartmentService : IDepartmentService
	{

		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}



		public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
		{
			var departments = _unitOfWork.DepartmentRepositry.GetAll().Where(d => !d.IsDeleted);

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
			var department = _unitOfWork.DepartmentRepositry.Get(id);
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
			_unitOfWork.DepartmentRepositry.Add(department);

			return _unitOfWork.Complete();

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
			_unitOfWork.DepartmentRepositry.Update(department);
			return _unitOfWork.Complete();
		}

		public bool DeleteDepartment(int id)
		{
			var department = _unitOfWork.DepartmentRepositry.Get(id);

			if (department is { })
			{
				var employees = _unitOfWork.EmployeeRepositry.GetAllIQueryable().Where(e => e.Department == department);

				foreach (var employee in employees)
				{
					employee.DepartmentId = null;
				}


				_unitOfWork.DepartmentRepositry.Delete(department);

			}

			return _unitOfWork.Complete() > 0;
		}

	}
}