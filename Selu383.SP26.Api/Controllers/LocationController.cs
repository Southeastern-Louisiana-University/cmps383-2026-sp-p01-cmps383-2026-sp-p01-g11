using Microsoft.AspNetCore.Mvc;

namespace Selu383.SP26.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{

    private readonly ILogger<LocationController> _logger;

    public LocationController(ILogger<LocationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetLocation")]
    public IEnumerable<Location> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Location
		{
            Name = "",
            Address = ""
		})
        .ToArray();
    }
}
