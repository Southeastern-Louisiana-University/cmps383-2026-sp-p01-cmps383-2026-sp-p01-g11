namespace Selu383.SP26.Api.Dtos;

public class LocationGetDto
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Address { get; set; }
	public int TableCount { get; set; }
}


public class LocationPutDto
{
	public string? Name { get; set; }
	public string? Address { get; set; }
	public int TableCount { get; set; }
}