using Microsoft.EntityFrameworkCore;
using TeamsApp.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://teamsapp-enss.onrender.com/")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html"); // for Blazor WASM client routing

if (app.Environment.IsDevelopment())
{
    app.Urls.Add("https://localhost:5001");
}
else
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    app.Urls.Add($"http://*:{port}");
}


app.Run();
