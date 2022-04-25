

#pragma warning disable CS8618

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
    public int type { get; set; }

    [MaxLength(4000)]
    public string message { 
        get
        {
            return this.message;
        } 

        set 
        {
            if(this.message != "COMMENT" || this.message != "AVAILIBILITY") {
                throw new Exception("Invalid message type");
            }
        }
    }

    
    public DateTime availibilityDate { get; set; }

    public DateTime? createdAt { 
        get
        {
            return this.createdAt.HasValue
                ? this.createdAt.Value
                : DateTime.Now;
        }

        set { this.createdAt = value; }
     }
}


