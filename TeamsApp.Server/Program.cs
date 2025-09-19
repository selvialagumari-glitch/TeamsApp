using Microsoft.EntityFrameworkCore;
using TeamsApp.Server.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy => policy.WithOrigins("https://teamsapp-enss.onrender.com/")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.UseHttpsRedirection();
app.UseCors("AllowClient");
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
