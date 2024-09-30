using LinkDev.IKEA.BLL.Common.Attchments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entites.Identity;
using LinkDev.IKEA.DAL.Presistance.Data;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Departments;
using LinkDev.IKEA.DAL.Presistance.Reposatories.Employees;
using LinkDev.IKEA.DAL.Presistance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LinkDev.IKEA.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

			builder.Services.AddScoped<IDepartmentRepositry, DepartmentRepositry>();
			builder.Services.AddScoped<IEmployeeRepositry, EmployeeRepositry>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			builder.Services.AddScoped<IEmployeeService, EmployeeService>();
			builder.Services.AddScoped<IAttachmentService, AttachmentService>();

			builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
				optionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
			{
				options.Password.RequiredLength = 5;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredUniqueChars = 1;


				options.User.RequireUniqueEmail = true;
				//options.User.AllowedUserNameCharacters = "12312imad,casld";


				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

			}).AddEntityFrameworkStores<ApplicationDbContext>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
