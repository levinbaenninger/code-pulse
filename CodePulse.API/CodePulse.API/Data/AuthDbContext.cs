using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// Create and seed roles
			var readerRoleId = "f7533f85-312c-4556-9968-4d69ff80b0ae";
			var writerRoleId = "f6de4425-fca8-4bfb-8dd9-5b3b30e68eec";

			var roles = new List<IdentityRole>
			{
				new IdentityRole()
				{
					Id = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper(),
					ConcurrencyStamp = readerRoleId
				},
				new IdentityRole()
				{
					Id = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper(),
					ConcurrencyStamp = writerRoleId
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);

			// Create and seed admin user
			var adminUserId = "d6cab684-3456-4242-8b88-c8951d26d216";

			var admin = new IdentityUser()
			{
				Id = adminUserId,
				UserName = "admin@codepulse.com",
				Email = "admin@codepulse.com",
				NormalizedEmail = "admin@codepulse.com".ToUpper(),
				NormalizedUserName = "admin@codepulse.com".ToUpper(),
			};

			admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

			builder.Entity<IdentityUser>().HasData(admin);


			// Assign admin user to roles
			var adminRoles = new List<IdentityUserRole<string>>
			{
				new()
				{
					UserId = adminUserId,
					RoleId = readerRoleId
				},
				new()
				{
					UserId = adminUserId,
					RoleId = writerRoleId
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

			base.OnModelCreating(builder);
		}
	}
}
