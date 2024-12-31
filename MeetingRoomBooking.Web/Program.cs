using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.Web.Models;
using Mapster;
using MeetingRoomBooking.Web.DI;
using MeetingRoomBooking.Web.MappingProfile;
using MeetingRoomBooking.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<testContext>(

    options => options.UseSqlServer(builder.Configuration.GetConnectionString(/*"localConn"*/"localDB"))

);

//DI
builder.Services.AddDependencyInjection(builder.Configuration);
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MapperConfig).Assembly);
builder.Services.AddSingleton(config);

builder.Services.AddSession(op =>
{
    op.IdleTimeout = TimeSpan.FromMinutes(20);
    op.Cookie.HttpOnly = true;
    op.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=booking}/{action=CreateNewBooking}/{id?}");

app.Run();
