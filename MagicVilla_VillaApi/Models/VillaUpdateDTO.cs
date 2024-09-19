using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models
{
	public class VillaUpdateDTO
	{
		public int? Id { get; set; }  // Make Id nullable
		[MaxLength(30)]
		public string? Name { get; set; }  // Nullable
		public string? Details { get; set; }  // Nullable
		public double? Rate { get; set; }  // Nullable
		public int? Sqft { get; set; }  // Nullable
		public int? Occupancy { get; set; }  // Nullable
		public string? ImageUrl { get; set; }  // Nullable
		public string? Amenity { get; set; }  // Nullable
	}
}
