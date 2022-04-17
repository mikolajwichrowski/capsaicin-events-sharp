using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;

public class UserReturnType
{
    public int id { get;set; } = 0;
    public string username { get;set; } = "";
}


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<UserReturnType> List()
    {
        IEnumerable<UserReturnType> users;
        using (var context = new AppContext())
        {
            users = context.Users.ToList().ConvertAll<UserReturnType>(user => new UserReturnType{ id = user.id, username = user.username });
        }
        return users;
    }
}
