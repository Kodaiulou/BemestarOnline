using Microsoft.AspNetCore.Mvc;

namespace BemEstarOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "online", sistema = "Bem Estar Online API", data = DateTime.UtcNow });
}
