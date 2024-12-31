using AutoMapper;
using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Implements;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.Implements;
using MeetingRoomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using prjMeetingRoomBooking.ViewModels;
using MeetingRoomBooking.Common.Models;
using prjMeetingRoomBooking.Mapping;
using CoreProfiler.Web;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<testContext>(
//    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("localDB"))
//);


var demoConnStr = builder.Configuration.GetConnectionString("localDB");
builder.Services.AddScoped<IDatabaseHelper>(x => new DatabaseHelper(demoConnStr));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddSession(op =>
{
    op.IdleTimeout = TimeSpan.FromMinutes(20);
    op.Cookie.HttpOnly = true;
    op.Cookie.IsEssential = true;
});


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseCoreProfiler(true);
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
