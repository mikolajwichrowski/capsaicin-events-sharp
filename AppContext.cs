using Microsoft.EntityFrameworkCore;
using capsaicin_events_sharp.Entities;


namespace capsaicin_events_sharp;

public class AppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public string DbPath { get; }

    public AppContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "dev.sqlite");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}