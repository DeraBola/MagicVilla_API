using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controllers
{
	// [Route("api/[controller]")]
	[Route("api/VillaAPI")]
	[ApiController]

	public class VillaAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		public VillaAPIController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<VillaDTO>> GetVillas()
		{
			return Ok(_db.Villas);
		}

		[HttpGet("{id:int}", Name = "GetVilla")]
		//  [ProducesResponseType(StatusCodes.Status200OK)]
		//  [ProducesResponseType(StatusCodes.Status400BadRequest)]
		//  [ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<VillaDTO> GetVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			return Ok(villa);
		}


		[HttpPost]
		// [ProducesResponseType(StatusCodes.Status201Created)]
		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
		{

			/*  if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                } */
			if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("CustomError", "Villa already Exists!");
				return BadRequest(ModelState);
			}

			if (villaDTO == null)
			{
				return BadRequest("Villa data is null.");
			}
			if (villaDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Invalid ID value.");
			}
			Villa model = new()
			{
				Name = villaDTO.Name,
				Amenity = villaDTO.Amenity,
				Sqft = villaDTO.Sqft,
				Occupancy = villaDTO.Occupancy,
				Details = villaDTO.Details,
				Id = villaDTO.Id,
				Rate = villaDTO.Rate,
				ImageUrl = villaDTO.ImageUrl,
			};
			_db.Add(model);
			_db.SaveChanges();
			return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
		}


		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		public IActionResult DeleteVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}

			_db.Villas.Remove(villa);
			_db.SaveChanges();
			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
		{
			if (villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}
			Villa model = new()
			{
				Name = villaDTO.Name,
				Amenity = villaDTO.Amenity,
				Sqft = villaDTO.Sqft,
				Occupancy = villaDTO.Occupancy,
				Details = villaDTO.Details,
				Id = villaDTO.Id,
				Rate = villaDTO.Rate,
				ImageUrl = villaDTO.ImageUrl,
			};
			_db.Villas.Update(model);
			_db.SaveChanges();
			return NoContent();
		}


		[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
		public IActionResult UpdatePartialVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
		{
			if (id == 0 || villaUpdateDTO == null)
			{
				return BadRequest();
			}

			var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
			if (villa == null)
			{
				return NotFound();
			}

			// Manually update only the non-null fields
			if (!string.IsNullOrWhiteSpace(villaUpdateDTO.Name))
			{
				villa.Name = villaUpdateDTO.Name;
			}

			if (!string.IsNullOrWhiteSpace(villaUpdateDTO.Details))
			{
				villa.Details = villaUpdateDTO.Details;
			}

			if (villaUpdateDTO.Rate.HasValue)
			{
				villa.Rate = villaUpdateDTO.Rate.Value;
			}

			if (villaUpdateDTO.Sqft.HasValue)
			{
				villa.Sqft = villaUpdateDTO.Sqft.Value;
			}

			if (villaUpdateDTO.Occupancy.HasValue)
			{
				villa.Occupancy = villaUpdateDTO.Occupancy.Value;
			}

			if (!string.IsNullOrWhiteSpace(villaUpdateDTO.ImageUrl))
			{
				villa.ImageUrl = villaUpdateDTO.ImageUrl;
			}

			if (!string.IsNullOrWhiteSpace(villaUpdateDTO.Amenity))
			{
				villa.Amenity = villaUpdateDTO.Amenity;
			}

			_db.Villas.Update(villa);
			_db.SaveChanges();

			return NoContent();
		}


	}
}
