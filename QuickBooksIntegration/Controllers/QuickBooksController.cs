using Infrastructure.Integrations.QuickBooks;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/quickbooks")]
public class QuickBooksController : ControllerBase
{
    private readonly QuickBooksAuthService _authService;

    public QuickBooksController(QuickBooksAuthService authService)
    {
        _authService = authService;
    }

    // Endpoint to get the QuickBooks authorization URL
    [HttpGet("auth-url")]
    public IActionResult GetAuthUrl()
    {
        var authUrl = _authService.GetAuthorizationUrl();
        return Ok(new { url = authUrl });
    }

    // Endpoint to handle the OAuth2 callback and exchange the auth code for tokens
    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return BadRequest(new { message = "Authorization code is missing." });
        }

        try
        {
            var tokenResponse = await _authService.ExchangeCodeForToken(code);
            return Ok(tokenResponse); // Return token details (access and refresh tokens)
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing the callback.", error = ex.Message });
        }
    }
}