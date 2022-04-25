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
                    creator = new UserResponseType{ id=row.creator.id, username=row.creator.username },
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
                creator=creator,
                description=eventRequest.description,
                picture=eventRequest.picture,
                location=eventRequest.location
            };
            context.Events.Add(newEvent);
            context.SaveChanges();
        }
        return newEvent;
    }

    [Route(":id/attendees")]
    [HttpGet(Name = "GetAttendees")]
    public IEnumerable<AttendeeResponseType> ListAttendees()
    {
        IEnumerable<AttendeeResponseType> attendees;

        using (var context = new AppContext())
        {
            attendees = context.Attendees
                .Include(row => row.user)
                .ToList()
                .ConvertAll(row => new AttendeeResponseType{
                    id = row.id,
                    user = new UserResponseType{ id=row.user.id, username=row.user.username }
                });
        }

        return attendees;
    }

    [Route("{id:int}/register")]
    [HttpGet(Name = "PostAttendees")]
    public AttendeeResponseType PostAttendees(int id, [FromBody] AttendeeRequestType attendeeRequest)
    {
        AttendeeResponseType attendee;

        using (var context = new AppContext())
        {

            User user = context.Users.Where(user => user.id == attendeeRequest.user).First();
            Event @event = context.Events.Where(@event => @event.id == id).First();
            Attendee newAttendee = new Attendee{
                user = user,
                @event = @event
            };
            context.Attendees.Add(newAttendee);
            context.SaveChanges();
            attendee = new AttendeeResponseType{
                id = newAttendee.id,
                user = new UserResponseType{ id=newAttendee.user.id, username=newAttendee.user.username }
            };
        }

        return attendee;
    }
}
