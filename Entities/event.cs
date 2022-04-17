using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace capsaicin_events_sharp.Entities;


public class Event
{
    public Event(User creator, string description, string picture, string location) {
        this.creator = creator;
        this.description = description;
        this.picture = picture;
        this.location = location;
    }

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


