using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Selu383.SP26.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{

    private readonly DataContext _dataContext;

    public LocationController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet(Name = "GetAllLocations")]
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

    [HttpPost(Name = "PostLocation")]
    public IActionResult Post(LocationPostDto locationDto)
    {
        var location = new Location
        {
            Name = locationDto.Name,
            Address = locationDto.Address,
            TableCount = locationDto.TableCount
        };
        _dataContext.Locations.Add(location);
        _dataContext.SaveChanges();
        return CreatedAtRoute("GetAllLocations", new { id = location.Id }, location);
    }

    [HttpPut(Name = "UpdateLocation")]
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
