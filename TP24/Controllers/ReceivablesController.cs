using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP24.Models;
using TP24.Services;

[ApiController]
[Route("[controller]")]
public class ReceivablesController : ControllerBase
{
    private readonly IReceivableService _receivableService;

    public ReceivablesController(IReceivableService receivableService)
    {
        _receivableService = receivableService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddReceivable(ReceivablePayload payload)
    {
        try
        {
            await _receivableService.AddReceivable(payload);
            return Ok("Receivable added successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetReceivableSummary()
    {
        var summary = await _receivableService.GetReceivableSummary();
        return Ok(summary);
    }
}
