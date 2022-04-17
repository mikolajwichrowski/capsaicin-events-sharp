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

        try {
            string loggedIn = context.Request.Cookies["logged_in"].ToString();
            string userId = context.Request.Cookies["user_id"].ToString();
            int userIdInt = Int32.Parse(userId);
            using (var dbContext = new AppContext())
            {
                dbContext.Users.Where(user => user.id == userIdInt).First();
                await this.next(context);
            }
        } catch {
            throw new UnauthorizedAccessException();
        }
    }
}