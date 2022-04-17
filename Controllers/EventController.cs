using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;

public class EventRequestData
{


    public int creator { get; set; }
    

    public string description { get; set; }


    public string picture { get; set; }

    public string location { get; set; }
}


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
    public IEnumerable<Event> List()
    {
        IEnumerable<Event> events;
        using (var context = new AppContext())
        {
            // TODO: MAP OUT THE PASSWORDS!!!
            events = context.Events.Include(row => row.creator).ToList();
        }
        return events;
    }

    [HttpPost]
    public Event Post([FromBody] EventRequestData eventRequestData)
    {
        Event newEvent;
        using (var context = new AppContext())
        {
            User creator = context.Users.Where(user => user.id == eventRequestData.creator).First();
            newEvent = new Event{
                description = eventRequestData.description,
                location = eventRequestData.description,
                picture = eventRequestData.description,
                creator = creator
            };
            context.Events.Add(newEvent);
            context.SaveChanges();
        }
        return newEvent;
    }
}
