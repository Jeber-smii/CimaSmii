using Gestion_de_films.Models;
using Gestion_de_films.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*-----------------------------------------------------------------------------*/

builder.Services.AddDbContextPool<MovieContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CimaSmiiDBConnection")));

builder.Services.AddScoped<IRoomRepository<Room>, RoomRepository>();

builder.Services.AddScoped<IMovieRepository <Movie>, MovieRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MovieContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

/*------------------------------------------------------------------------------*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    /* ---------------------------------*/
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
