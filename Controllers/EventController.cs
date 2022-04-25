using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
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
    public Event Post([FromBody] EventRequestType eventRequest, HttpContext context)
    {
        int userId = int.Parse(context.Request.Cookies["user_id"]);
        Event newEvent;
        using (var context = new AppContext())
        {
            User creator = context.Users.Where(user => user.id == userId).First();
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

    [Route(":id/files")]
    [HttpGet(Name = "GetAttendees")]
    public IEnumerable<EventFileResponseType> ListEventFiles()
    {
        IEnumerable<EventFileResponseType> eventFiles;

        using (var context = new AppContext())
        {
            eventFiles = context.EventFiles
                .ToList()
                .ConvertAll(row => new EventFileResponseType{
                    id = row.id,
                    @event = row.@event.id,
                    fileLocation = row.fileLocation
                });
        }

        return eventFiles;
    }

    [HttpPost(Name="Upload"), DisableRequestSizeLimit]
    [Route("{id:int}/upload")]
    public EventFileResponseType Upload(int id)
    {
        EventFileResponseType eventFile;
        var file = Request.Form.Files[0];
        var folderName = Path.Combine("Resources", "Images");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            using (var context = new AppContext()) {
                Event @event = context.Events.Where(@event => @event.id == id).First();
                EventFile newFile = new EventFile{
                    @event=@event,
                    fileLocation = dbPath
                };
                context.EventFiles.Add(newFile);
                context.SaveChanges();
                eventFile = new EventFileResponseType{
                    id = newFile.id,
                    @event = newFile.@event.id,
                    fileLocation = newFile.fileLocation
                };
            }
            return eventFile;
        }

        throw new BadHttpRequestException("Internal error");
    }

    [Route("{id:int}/react")]
    [HttpPost(Name="CreateReaction")]
    public void CreateReaction([FromBody] EventRequestType eventRequest, HttpContext context)
    {
        // TODO: make a reaction
        int userId = int.Parse(context.Request.Cookies["user_id"]);
        // Event newEvent;
        // using (var context = new AppContext())
        // {
        //     User creator = context.Users.Where(user => user.id == userId).First();
        //     newEvent = new Event{
        //         creator=creator,
        //         description=eventRequest.description,
        //         picture=eventRequest.picture,
        //         location=eventRequest.location
        //     };
        //     context.Events.Add(newEvent);
        //     context.SaveChanges();
        // }
        // return newEvent;
    }

    [Route("{id:int}/react")]
    [HttpGet(Name="ListReactions")]
    public void ListReactions([FromBody] EventRequestType eventRequest, HttpContext context)
    {
        // TODO: get all reactions
        int userId = int.Parse(context.Request.Cookies["user_id"]);
        // Event newEvent;
        // using (var context = new AppContext())
        // {
        //     User creator = context.Users.Where(user => user.id == userId).First();
        //     newEvent = new Event{
        //         creator=creator,
        //         description=eventRequest.description,
        //         picture=eventRequest.picture,
        //         location=eventRequest.location
        //     };
        //     context.Events.Add(newEvent);
        //     context.SaveChanges();
        // }
        // return newEvent;
    }
}
