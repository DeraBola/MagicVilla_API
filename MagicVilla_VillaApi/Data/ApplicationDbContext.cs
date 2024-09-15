using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Villa> Villas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Villa>().HasData(
				new Villa
				{
					Id = 1,
					Name = "Royal Villa",
					Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.\r\n",
					ImageUrl = "https://th.bing.com/th/id/OIP.87rC-vQdkf1I5qv74_2LjwHaHp?rs=1&pid=ImgDetMain",
					Rate = 200,
					Sqft = 550,
					Amenity = ""
				},
	new Villa
	{
		Id = 2,
		Name = "Luxury Villa",
		Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent scelerisque odio vitae metus aliquet, vel fermentum quam lacinia. Nulla facilisi. Curabitur ac turpis ut erat varius lacinia.\r\n",
		ImageUrl = "https://th.bing.com/th/id/R.1a616d03469304f3ee855e44e1037918?rik=pFgQjXyVUHRqoA&riu=http%3a%2f%2fupload.wikimedia.org%2fwikipedia%2fcommons%2f3%2f38%2fFlower_July_2011-2_1_cropped.jpg&ehk=I18Ym0u7Qb7y5%2bz5oa87N%2bbaWjnVGYuMrN6djhH6O9I%3d&risl=&pid=ImgRaw&r=0",
		Rate = 300,
		Sqft = 750,
		Amenity = "Private pool, Jacuzzi"
	},
	new Villa
	{
		Id = 3,
		Name = "Beachfront Villa",
		Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce dapibus turpis ut egestas hendrerit. Aenean vitae dictum libero. Aliquam erat volutpat. Vivamus vestibulum nisi non magna viverra sollicitudin.\r\n",
		ImageUrl = "https://th.bing.com/th/id/OIP.Vtxy0FjT_EfudI4cQk1kzAHaE8?rs=1&pid=ImgDetMain",
		Rate = 350,
		Sqft = 850,
		Amenity = "Ocean view, Private beach"
	},
	new Villa
	{
		Id = 4,
		Name = "Mountain Retreat",
		Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel justo nec nisi dignissim condimentum. Sed aliquet libero eu felis vulputate, id vehicula odio efficitur.\r\n",
		ImageUrl = "https://th.bing.com/th/id/R.1a616d03469304f3ee855e44e1037918?rik=pFgQjXyVUHRqoA&riu=http%3a%2f%2fupload.wikimedia.org%2fwikipedia%2fcommons%2f3%2f38%2fFlower_July_2011-2_1_cropped.jpg&ehk=I18Ym0u7Qb7y5%2bz5oa87N%2bbaWjnVGYuMrN6djhH6O9I%3d&risl=&pid=ImgRaw&r=0",
		Rate = 400,
		Sqft = 950,
		Amenity = "Mountain view, Fireplace"
	}
				);
		}
	}
}
