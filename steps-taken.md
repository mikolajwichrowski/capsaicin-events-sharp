# 0 Architecture
0. Install all packages as described https://docs.microsoft.com/nl-nl/dotnet/core/install/linux-ubuntu
1. Create the project with `dotnet new webapi` https://www.freecodecamp.org/news/an-awesome-guide-on-how-to-build-restful-apis-with-asp-net-core-87b818123e28/
2. Start the app with `dotnet watch run` to test and go to https://localhost:5199/weatherforecast
3. Add sqlite for entity framework `dotnet add package Microsoft.EntityFrameworkCore.Sqlite` and make context as described here https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
4. install dotnet ef tools `dotnet tool install --global dotnet-ef` and add to path with `export PATH="$PATH:/home/gitpod/.dotnet/tools"` docs: https://docs.microsoft.com/en-us/ef/core/cli/dotnet
5. install ef packages `dotnet add package Microsoft.EntityFrameworkCore.Design`
6. Create user controller and entity and run `dotnet ef migrations add InitialCreate` and `dotnet ef database update` to create the db
7. Create the authentication controller to allow login using cookies https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-6.0
8. Create authorization policy to make sure cookies are set https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.1#apply-policies-to-mvc-controllers

---
# 1 Event creation
1. Create event entity
2. Create event controller and types

---
# 2 Adding attendees
1. Create attendee entity. (event is een gereserveerd keyword)
2. Create attendee controller and types