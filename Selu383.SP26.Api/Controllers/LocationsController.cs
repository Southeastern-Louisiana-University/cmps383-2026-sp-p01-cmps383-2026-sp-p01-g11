using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Dtos;

namespace Selu383.SP26.Api.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
{

	private readonly DataContext _dataContext;

	public LocationsController(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	[HttpGet(Name = "GetAllLocation")]
	public IActionResult GetAll()
	{
		var result = _dataContext.Locations.Select(x => new LocationGetDto
		{
			Id = x.Id,
			Name = x.Name,
			Address = x.Address,
			TableCount = x.TableCount
		}).ToList();
		return Ok(result);
	}

	[HttpGet("{id}", Name = "GetLocationById")]
	public IActionResult GetById(int id)
	{
		var location = _dataContext.Locations.Find(id);
		if (location == null)
		{
			return NotFound();
		}

		var dto = new LocationGetDto
		{
			Id = location.Id,
			Name = location.Name,
			Address = location.Address,
			TableCount = location.TableCount
		};

		return Ok(dto);
	}

	[HttpPost(Name = "PostLocation")]
	public IActionResult Post(LocationPostDto locationDto)
	{
		var entity = new Locations
		{
			Name = locationDto.Name,
			Address = locationDto.Address,
			TableCount = locationDto.TableCount
		};

		if (entity.TableCount < 1)
		{
			return BadRequest("TableCount cannot be less than one.");
		}
		else if (entity.Name == null || entity.Name == "")
		{
			return BadRequest("Name cannot be empty.");
		}
		else if (entity.Name.Length > 100)
		{
			return BadRequest("Name cannot exceed 100 characters.");
		}
		else if (entity.Address == null || entity.Address == "")
		{
			return BadRequest("Address cannot be empty.");
		}
		else
		{
			_dataContext.Locations.Add(entity);
			_dataContext.SaveChanges();
		}
		
		// Route to the new location, return 201
		return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
	}

	[HttpPut(Name = "UpdateLocation")]
	public IActionResult Put(string name, LocationPutDto locationDto)
	{
		var location = _dataContext.Locations.Find(name);
		if (location == null)
		{
			return NotFound();
		}
		location.Name = locationDto.Name;
		location.Address = locationDto.Address;
		location.TableCount = locationDto.TableCount;
		_dataContext.SaveChanges();
		return NoContent();
	}

	[HttpDelete(Name = "DeleteLocation")]
	public IActionResult Delete(int id)
	{
		var location = _dataContext.Locations.Find(id);
		if (location == null)
		{
			return NotFound();
		}

		_dataContext.Locations.Remove(location);
		_dataContext.SaveChanges();

		return NoContent();
	}
}