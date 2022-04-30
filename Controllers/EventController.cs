using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class EventController : Controller
{
    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
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
    public EventResponseType Post([FromBody] EventRequestType eventRequest)
    {
        int userId = int.Parse(HttpContext.Request.Cookies["user_id"]);
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
        return new EventResponseType{
            creator=new UserResponseType{
                id=newEvent.creator.id,
                username=newEvent.creator.username,
            },
            description=newEvent.description,
            picture=newEvent.picture,
            location=newEvent.location
        };
    }

    
    [HttpGet(":id/attendees")]
    public IEnumerable<AttendeeResponseType> ListAttendees([FromRoute] int id)
    {
        IEnumerable<AttendeeResponseType> attendees;

        using (var context = new AppContext())
        {
            attendees = context.Attendees
                .Include(row => row.user)
                .Where(@event => @event.id == id)
                .ToList()
                .ConvertAll(row => new AttendeeResponseType{
                    id = row.id,
                    user = new UserResponseType{ id=row.user.id, username=row.user.username }
                });
        }

        return attendees;
    }

    [HttpPost("{id:int}/register")]
    public AttendeeResponseType PostAttendees([FromRoute] int id, [FromBody] AttendeeRequestType attendeeRequest)
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

    [HttpGet(":id/files")]
    public IEnumerable<EventFileResponseType> ListEventFiles([FromRoute] int id)
    {
        IEnumerable<EventFileResponseType> eventFiles;

        using (var context = new AppContext())
        {
            eventFiles = context.EventFiles
                .Where(@event => @event.id == id)
                .ToList()
                .ConvertAll(row => new EventFileResponseType{
                    id = row.id,
                    @event = row.@event.id,
                    fileLocation = row.fileLocation
                });
        }

        return eventFiles;
    }

    [HttpPost("{id:int}/upload")]
    public EventFileResponseType Upload([FromRoute] int id)
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

    [HttpPost("{id:int}/react")]
    public ReactionResponseType CreateReaction([FromRoute] int id, [FromBody] ReactionRequestType reactionRequest)
    {
        int userId = int.Parse(HttpContext.Request.Cookies["user_id"]);
        Reaction newReaction;
        using (var context = new AppContext())
        {
            User user = context.Users.Where(user => user.id == userId).First();
            Event @event = context.Events.Where(@event => @event.id == id).First();
            newReaction = new Reaction{
                user=user,
                @event=@event,
                type=reactionRequest.type,
                message=reactionRequest.message,
                availibilityDate=reactionRequest.availibilityDate
            };
            context.Reactions.Add(newReaction);
            context.SaveChanges();
        }
        return new ReactionResponseType{
            id=newReaction.id,
            @event=newReaction.@event.id,
            availibilityDate=newReaction.availibilityDate,
            createdAt=newReaction.createdAt,
            message=newReaction.message,
            type=newReaction.type,
            user=new UserResponseType{
                id=newReaction.user.id,
                username=newReaction.user.username
            }
        };
    }

    [HttpGet("{id:int}/reactions")]
    public IEnumerable<ReactionResponseType> ListReactions([FromRoute] int id, [FromBody] EventRequestType eventRequest)
    { 
        IEnumerable<ReactionResponseType> eventReactions;
        using (var context = new AppContext())
        {
            eventReactions = context.Reactions
                .Where(@event => @event.id == id)
                .Include(row => row.user)
                .ToList()
                .ConvertAll(row => new ReactionResponseType{
                    id=row.id,
                    @event=row.@event.id,
                    availibilityDate=row.availibilityDate,
                    createdAt=row.createdAt,
                    message=row.message,
                    type=row.type,
                    user=new UserResponseType{
                        id=row.user.id,
                        username=row.user.username
                    }
                });
        }
        return eventReactions;
    }
}
