using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodePulse.API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
				v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				foreach (var property in entityType.GetProperties())
				{
					if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
					{
						property.SetValueConverter(dateTimeConverter);
					}
				}
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
