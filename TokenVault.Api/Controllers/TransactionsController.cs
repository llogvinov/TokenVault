using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokenVault.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    [HttpGet]
    public IActionResult ListTransactions()
    {
        return Ok(Array.Empty<string>());
    }
}