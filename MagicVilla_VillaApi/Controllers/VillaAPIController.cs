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
		public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}

			// Get the villa entity from the database
			var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

			// Null check for villa after retrieval
			if (villa == null)
			{
				return NotFound(); // Return 404 if the villa is not found
			}

			// Map the villa to VillaDTO
			VillaDTO villaDTO = new()
			{
				Name = villa.Name,
				Amenity = villa.Amenity,
				Sqft = villa.Sqft,
				Occupancy = villa.Occupancy,
				Details = villa.Details,
				Id = villa.Id,
				Rate = villa.Rate,
				ImageUrl = villa.ImageUrl,
			};

			// Apply the JSON Patch to the VillaDTO
			patchDTO.ApplyTo(villaDTO, ModelState);

			// Check for model state errors after applying the patch
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Map the patched VillaDTO back to the Villa model
			villa.Name = villaDTO.Name;
			villa.Amenity = villaDTO.Amenity;
			villa.Sqft = villaDTO.Sqft;
			villa.Occupancy = villaDTO.Occupancy;
			villa.Details = villaDTO.Details;
			villa.Rate = villaDTO.Rate;
			villa.ImageUrl = villaDTO.ImageUrl;

			// Update the villa in the database
			_db.Villas.Update(villa);
			_db.SaveChanges();

			return NoContent(); // Return 204 when the patch is successfully applied
		}

	}
}
