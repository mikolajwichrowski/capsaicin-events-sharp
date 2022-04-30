namespace capsaicin_events_sharp.Controllers;

public class ReactionRequestType
{
    public string? message { get; set; }
    public string type { get; set; } = "COMMENT";
    public DateTime? availibilityDate { get; set; }
}