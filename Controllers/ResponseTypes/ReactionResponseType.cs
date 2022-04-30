namespace capsaicin_events_sharp.Controllers;

public class ReactionResponseType
{
    public int? id { get; set; }
    public int? @event { get; set; }
    public UserResponseType? user { get; set; }
    public string? message { get; set; }
    public string? type { get; set; }
    public DateTime? availibilityDate { get; set; }
    public DateTime? createdAt { get; set; }
}
