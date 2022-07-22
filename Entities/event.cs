#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace capsaicin_events_sharp.Entities;


public class Event
{

    [Key]
    public int id { get; set; }

    [Required]    
    public User creator { get; set; }
    
    [MaxLength(255)]
    public string description { get; set; }

    [MaxLength(4000)]
    public string picture { get; set; }

    [MaxLength(255)]
    public string location { get; set; }
    
}


