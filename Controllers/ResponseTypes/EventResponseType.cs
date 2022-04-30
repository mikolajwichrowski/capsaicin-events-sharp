namespace capsaicin_events_sharp.Controllers;

public class EventResponseType
{
    public int? id { get; set; }
    public UserResponseType? creator { get; set; }
    public string? description { get; set; }
    public string? picture { get; set; }
    public string? location { get; set; }
}
