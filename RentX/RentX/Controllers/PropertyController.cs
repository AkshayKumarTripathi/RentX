using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RentX.Handlers;
using RentX.Models;
using RentX.RentXDTOs;
namespace RentX.Controllers;

[Route("api/[controller]")]
public class PropertyController : Controller
{
    public readonly RentXContext context;
    public PropertyHandler PropertyHandler;

    // Constructor to inject PropertyHandler
    public PropertyController(RentXContext _db)
    {
        context = _db;
        PropertyHandler = new PropertyHandler(context);
    }

    #region Get Property Details
    [HttpGet("{id}")]
    public IActionResult GetProperty(int id)
    {
        GetPropertyDTO? details = PropertyHandler.GetPropertyDetails(id);
        if (details == null)
        {
            return BadRequest("Not found any property with the given property Id.");
        }
 
        return Json(details);
    }
    #endregion

    //Get All
    [HttpGet]
    public async Task<IActionResult> GetProperties()
    {
        List<GetPropertyDTO> properties = new List<GetPropertyDTO> ();
        properties = await PropertyHandler.GetAllProperties();
        return Json(properties);
    }

    // Post
    [HttpPost("create")]
    public async Task<IActionResult> createProperty([FromBody] PostPropertyDTO dto)
    {
        int propertyId = await PropertyHandler.CreateProperty(dto);
        if (propertyId == -1)
        {
            return BadRequest("Request Failed, Please pass Valid UserId and PropertyType");
        }

        return CreatedAtAction(nameof(GetProperty), new { id = propertyId }, propertyId);
    }

    // Delete
    [HttpDelete("{id}")]
    public IActionResult DeleteProperty(int id)
    {
        bool success = PropertyHandler.DeleteProperty(id);
        if (success)
        {
            return Ok("Property deleted successfully."); 
        }

        return BadRequest("Enter Correct PropertyId");
    }

    // Put
    [HttpPut("{id}")]
    public IActionResult ModifyProperty(int id,[FromBody] PostPropertyDTO request)
    {
        bool res = PropertyHandler.ModifyProperty(id, request);
        if (res) {
            return Ok("Modified Successfully");
        }
        return BadRequest("Please enter correct Values");
    }

}
