using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;


[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetEvents")]
    public IEnumerable<EventResponseType> List()
    {
        IEnumerable<EventResponseType> events;
        using (var context = new AppContext())
        {
            events = context.Events
                .Include(row => row.creator)
                .ToList()
                .ConvertAll(row => new EventResponseType{
                    creator = new UserResponseType{id=row.creator.id, username=row.creator.username},
                    description = row.description,
                    picture = row.picture,
                    location = row.location,
                });
        }
        return events;
    }

    [HttpPost]
    public Event Post([FromBody] EventRequestType eventRequest)
    {
        Event newEvent;
        using (var context = new AppContext())
        {
            User creator = context.Users.Where(user => user.id == eventRequest.creator).First();
            newEvent = new Event{
                description = eventRequest.description,
                location = eventRequest.description,
                picture = eventRequest.description,
                creator = creator
            };
            context.Events.Add(newEvent);
            context.SaveChanges();
        }
        return newEvent;
    }
}
