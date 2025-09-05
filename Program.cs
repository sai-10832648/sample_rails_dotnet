using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<sample_rails_app_8th_edNT.Models.ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "root",
    pattern: "",
    defaults: new { controller = "StaticPages", action = "Home" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Static pages
app.MapControllerRoute(
    name: "home",
    pattern: "home",
    defaults: new { controller = "StaticPages", action = "Home" });
app.MapControllerRoute(
    name: "help",
    pattern: "help",
    defaults: new { controller = "StaticPages", action = "Help" });
app.MapControllerRoute(
    name: "about",
    pattern: "about",
    defaults: new { controller = "StaticPages", action = "About" });
app.MapControllerRoute(
    name: "contact",
    pattern: "contact",
    defaults: new { controller = "StaticPages", action = "Contact" });

// Users
app.MapControllerRoute(
    name: "users",
    pattern: "users/{action=Index}/{id?}",
    defaults: new { controller = "Users" });
app.MapControllerRoute(
    name: "signup",
    pattern: "signup",
    defaults: new { controller = "Users", action = "New" });

// Sessions
app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Sessions", action = "Login" });
app.MapControllerRoute(
    name: "logout",
    pattern: "logout",
    defaults: new { controller = "Sessions", action = "Logout" });

// Microposts
app.MapControllerRoute(
    name: "microposts",
    pattern: "microposts/{action=Create}/{id?}",
    defaults: new { controller = "Microposts" });

// Relationships
app.MapControllerRoute(
    name: "relationships",
    pattern: "relationships/{action=Create}/{id?}",
    defaults: new { controller = "Relationships" });

// Password resets
app.MapControllerRoute(
    name: "passwordresets",
    pattern: "password_resets/{action=New}/{id?}",
    defaults: new { controller = "PasswordResets" });

// Account activations
app.MapControllerRoute(
    name: "accountactivations",
    pattern: "account_activations/{action=Edit}/{id?}",
    defaults: new { controller = "AccountActivations" });

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
