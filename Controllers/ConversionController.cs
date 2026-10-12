using Microsoft.AspNetCore.Mvc;
using UnitConverterApi.Models;
using UnitConverterApi.Services;

namespace UnitConverterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _service;

    public ConversionController(IConversionService service)
    {
        _service = service;
    }

    [HttpGet("categories")]
    public IActionResult GetAllCategories()
    {
        var response = _service.GetAllCategories();
         if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);

    }

    [HttpGet("categories/{category}/units")]
    public IActionResult GetAllUnitsInCategory(string category)
    {
        var response = _service.GetAllUnitsInCategory(category);
         if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Convert(ConversionRequest request)
    {
        var response = _service.Convert(
            request.Category,
            request.FromUnit,
            request.ToUnit,
            request.Value
        );

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
