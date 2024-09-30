using Microsoft.AspNetCore.Identity;

namespace LinkDev.IKEA.DAL.Entites.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string FName { get; set; } = null!;

		public string LName { get; set; } = null!;

		public bool IAgree { get; set; }


	}
}