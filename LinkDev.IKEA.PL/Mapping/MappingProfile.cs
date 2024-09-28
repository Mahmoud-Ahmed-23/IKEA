using AutoMapper;
using LinkDev.IKEA.BLL.Models.Department;
using LinkDev.IKEA.BLL.Models.Employee;
using LinkDev.IKEA.PL.ViewModels.Departments;

namespace LinkDev.IKEA.PL.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			#region Employee

			CreateMap<EmployeeDetailsToReturnDto, UpdatedEmployeeDto>();

			CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

			CreateMap<DepartmentViewModel, CreatedDepartmentDto>();

			#endregion

			#region Department

			CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModel>();

			CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

			CreateMap<DepartmentViewModel, CreatedDepartmentDto>();

			#endregion
		}
	}
}
