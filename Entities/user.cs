using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace capsaicin_events_sharp.Entities;

public class User
{
    public User(string username, string password) {
        this.username = username;
        this.password = password;
    }

    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}


