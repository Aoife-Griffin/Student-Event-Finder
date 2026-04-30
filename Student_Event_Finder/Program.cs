using Microsoft.EntityFrameworkCore;
using Student_Event_Finder.Data;
using Student_Event_Finder.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Events.Any())
{
    context.Events.AddRange(
        new Event
        {
            Title = "Freshers Week",
            Description = "Welcome event for new students",
            Category = "Campus Life",
            Date = DateTime.UtcNow.AddDays(7),
            Time = "18:00",
            Location = "TU Dublin Main Hall",
            ImageUrl = "https://example.com/freshers.jpg",
            Organizer = "Student Union"
        },

        new Event
        {
            Title = "Career Fair",
            Description = "Meet top employers and recruiters",
            Category = "Careers",
            Date = DateTime.UtcNow.AddDays(14),
            Time = "10:00",
            Location = "Conference Centre",
            ImageUrl = "https://example.com/careerfair.jpg",
            Organizer = "Careers Office"
        },

        new Event
        {
            Title = "Hackathon",
            Description = "24-hour coding competition",
            Category = "Technology",
            Date = DateTime.UtcNow.AddDays(21),
            Time = "09:00",
            Location = "Computer Science Building",
            ImageUrl = "https://example.com/hackathon.jpg",
            Organizer = "Computer Society"
        }
    );

    context.SaveChanges();
}
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Urls.Add($"http://*:{port}");

app.Run();