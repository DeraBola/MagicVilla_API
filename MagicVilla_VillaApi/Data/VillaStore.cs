using MagicVilla_VillaApi.Models.Dto;

namespace MagicVilla_VillaApi.Data
{
	public static class VillaStore
	{
		public static List<VillaDTO> villaList = new List<VillaDTO>
			{
				new VillaDTO { Id = 1, Name = "Pool View", Occupancy=10, Sqft=540 },
				new VillaDTO { Id = 2, Name = "Palm View", Occupancy=5, Sqft=300 },
				new VillaDTO { Id = 3, Name = "Beach View", Occupancy=13, Sqft=780 }
			};
	}
}
