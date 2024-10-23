using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Authentication.Commands.Register;
using TokenVault.Application.Authentication.Queries.Login;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Contracts.Authentication;

namespace TokenVault.Api.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    /// <summary>
    ///     Get BTC price
    /// </summary>
    /// <returns></returns>
    [HttpPost("btc")]
    public async Task<IActionResult> GetBTCPrice()
    {
        try
        {
            // Send a GET request to the URL
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://cryptoprices.cc/BTC");
            
            // Parse the result into a decimal (since the response is a simple number)
            if (decimal.TryParse(response, out var btcPrice))
            {
                return Ok(btcPrice);
            }
            else
            {
                throw new Exception("Invalid response format from BTC price endpoint.");
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle any request-related errors
            throw new Exception("Error fetching BTC price.", ex);
        }
    }
}