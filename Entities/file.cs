#pragma warning disable CS8618

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace capsaicin_events_sharp.Entities;


public class EventFile
{

    [Key]
    public int id { get; set; }

    [Required]    
    public Event @event { get; set; }

    [MaxLength(255)]
    public string fileLocation { get; set; }

}


