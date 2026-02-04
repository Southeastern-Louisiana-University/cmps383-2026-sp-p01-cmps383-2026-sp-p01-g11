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
		var location = new Location
		{
			Name = locationDto.Name,
			Address = locationDto.Address,
			TableCount = locationDto.TableCount
		};

		if (location.TableCount < 1)
		{
			return BadRequest("TableCount cannot be less than one.");
		}
		else if (location.Name == null || location.Name == "")
		{
			return BadRequest("Name cannot be empty.");
		}
		else if (location.Name.Length > 100)
		{
			return BadRequest("Name cannot exceed 100 characters.");
		}
		else if (location.Address == null || location.Address == "")
		{
			return BadRequest("Address cannot be empty.");
		}
		else
		{
			_dataContext.Locations.Add(location);
			_dataContext.SaveChanges();
		}

		// Route to the new location, return 201
		return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
	}

	[HttpPut("{id}", Name = "UpdateLocation")]
	public IActionResult Put(int id, LocationPutDto locationDto)
	{
		var location = _dataContext.Locations.Find(id);
		if (location == null)
		{
			return NotFound();
		}

		location.Name = locationDto.Name;
		location.Address = locationDto.Address;
		location.TableCount = locationDto.TableCount;

		if (location.TableCount < 1)
		{
			return BadRequest("TableCount cannot be less than one.");
		}
		else if (location.Name == null || location.Name == "")
		{
			return BadRequest("Name cannot be empty.");
		}
		else if (location.Name.Length > 100)
		{
			return BadRequest("Name cannot exceed 100 characters.");
		}
		else if (location.Address == null || location.Address == "")
		{
			return BadRequest("Address cannot be empty.");
		}
		else
		{
			_dataContext.SaveChanges();
			return Ok();
		}

		return Ok();
	}

	[HttpDelete("{id}", Name = "DeleteLocation")]
	public IActionResult Delete(int id)
	{
		var location = _dataContext.Locations.Find(id);
		if (location == null)
		{
			location = _dataContext.Locations.Find(id); 
			if (location == null)
			{
				return NotFound();
			}
		}

		_dataContext.Locations.Remove(location);
		_dataContext.SaveChanges();

		return Ok();
	}
}