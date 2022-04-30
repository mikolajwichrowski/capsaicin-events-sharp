#pragma warning disable CS8602

using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp;

public class Middleware {
    private readonly RequestDelegate next;

    public Middleware(RequestDelegate next) {
        this.next = next;
    }

    public async Task Invoke(HttpContext context) {
        string route = context.Request.Path.ToString();
        bool isRegister = route.Contains("register");
        bool isAuthenticate = route.Contains("authenticate");

        if(isRegister || isAuthenticate) {
            await this.next(context);
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
        catch(Exception error) {
            Console.Write(error);
            throw new UnauthorizedAccessException();
        }
        finally {
            if(user != null) {
                await this.next(context);
            }
        }
    }
}