using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Data.Configurations.Employees
{
	internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

			builder.Property(e => e.Gender).HasConversion(

				(gender) => gender.ToString(),
				(gender) => (Gender)Enum.Parse(typeof(Gender), gender)
				);

			builder.Property(e => e.EmployeeType).HasConversion(

				(type) => type.ToString(),
				(type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
				);

			builder.Property(E => E.CreatedOn).HasDefaultValueSql("GetDate()");

			builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GetDate()");

		}
	}
}
