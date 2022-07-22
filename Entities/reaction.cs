#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace capsaicin_events_sharp.Entities;

public class Reaction
{

    [Key]
    public int id { get; set; }

    [Required]    
    public User user { get; set; }

    [Required]    
    public Event @event { get; set; }

    [MaxLength(255)]
    public string type { get; set; }

    [MaxLength(4000)]
    public string? message { get; set; }

    public DateTime? availibilityDate { get; set; }

    public DateTime? createdAt { get; set; } = DateTime.Now;
    
}


