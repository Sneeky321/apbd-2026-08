using Microsoft.AspNetCore.Mvc;
using apbd_2026_08.Services;
using apbd_2026_08.DTOs;

namespace apbd_2026_08.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcsController : ControllerBase
{
    private readonly IDbService _service;
    
    public PcsController(IDbService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        
        return Ok(result);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetComponents([FromRoute] int id)
    {
        var result = await _service.GetComponentsAsync(id);

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePcDto dto)
    {
        var result = await _service.CreateAsync(dto);
        
        return CreatedAtAction(
            nameof(GetAll),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreatePcDto dto)
    {
        var success =  await _service.UpdateAsync(id, dto);

        if (!success)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var success = await _service.DeleteAsync(id);
        
        if(!success)
            return NotFound();

        return NoContent();
    }
}