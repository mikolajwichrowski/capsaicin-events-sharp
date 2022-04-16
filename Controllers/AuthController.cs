using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public AuthController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Authenticate")]
    public void Authenticate()
    {
        
    }

    [HttpGet(Name = "Register")]
    public void Register()
    {
        
    }
}
