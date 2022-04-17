using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace capsaicin_events_sharp.Entities;

[Index(nameof(username), IsUnique = true)]
public class User
{
    public User(string username, string password) {
        this.username = username;
        this.password = password;
    }

    public int id { get; set; }

    [Required]    
    public string username { get; set; }
    
    [Required]
    public string password { get; set; }
}


