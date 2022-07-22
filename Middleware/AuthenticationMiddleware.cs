#pragma warning disable CS8602

using capsaicin_events_sharp.Entities;
using System.Net;

namespace capsaicin_events_sharp;

public class AuthenticationMiddleware {
    private readonly RequestDelegate _next;       
    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
        string route = context.Request.Path.ToString();
        bool isRegister = route.Contains("register");
        bool isAuthenticate = route.Contains("authenticate");

        if(isRegister || isAuthenticate) {
            await _next.Invoke(context);
            return;
        }
        
        User? user = null;
        try {
            string loggedIn = context.Request.Cookies["logged_in"].ToString();
            string userId = context.Request.Cookies["user_id"].ToString();
            int userIdInt = Int32.Parse(userId);
            using (var dbContext = new AppContext())
            {
                user = dbContext.Users.Find(userIdInt);
            }   
        }
        catch(Exception) {
            throw new HttpError("Unauthorized", HttpStatusCode.Unauthorized);
        }
        
        if(user != null) {
            await _next.Invoke(context);
            return;
        } else {
            throw new HttpError("Unauthorized", HttpStatusCode.Unauthorized);
        }
    }
}