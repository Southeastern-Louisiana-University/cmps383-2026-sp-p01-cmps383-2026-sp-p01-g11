using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{

	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder
			
			.UseSeeding((context, _) =>
			{
				var testLocation = context.Set<Location>().FirstOrDefault(b => b.Id == 1);
				if (!context.Set<Location>().Any())
				{
					context.Set<Location>().Add(new Location
					{
						Name = "Southeastern",
						Address = "123 address"
					});
					context.SaveChanges();
				}
			});
	public DbSet<Location> Locations => Set<Location>();
}
