using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<capsaicin_events_sharp.AuthenticationMiddleware>();
app.UseMiddleware<capsaicin_events_sharp.ExceptionMiddleware>();

var folderName = "uploads";
var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

app.UseFileServer(new FileServerOptions
{    
    FileProvider = new PhysicalFileProvider(
           pathToSave),
    RequestPath = "/uploads",
    EnableDirectoryBrowsing = true
});


app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
