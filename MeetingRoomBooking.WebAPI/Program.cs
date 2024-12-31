using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Implements;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Service.Implements;
using MeetingRoomBooking.Service.Interfaces;
using Evertrust.ResponseWrapper.Extensions;
using Evertrust.ResponseWrapper.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Evertrust.Core.Logging.Abstractions;
using CoreProfiler.Web;
using MeetingRoomBooking.Common.Infrastructure.Middlewares;
using MeetingRoomBooking.WebAPI.Mapping;
using Microsoft.OpenApi.Models;
using Exceptionless;
using FluentValidation.AspNetCore;
using MeetingRoomBooking.Common.Infrastructure.Validators;
using System.Reflection;
using Evertrust.Core.Logging.Exceptionless.DependencyInjection;
using Evertrust.Core.Logging.Exceptionless;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Filters;
using MeetingRoomBooking.Common.Models;
using AutoMapper.Configuration.Annotations;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    option => { 
        option.AddEvertrustResponseWrapperFilters();
        option.Filters.Add(typeof(ExceptionFilter));
    }
).ConfigureApiBehaviorOptions(option=>
{
    option.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "MeetingRoomBooking - V1",
            Version = "v1"
        }
     );

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.ExampleFilters();
});
// Swashbuckle.AspNetCore.Filters Add Swagger Examples
//builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<BookingInputParamater>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<BookingViewModel>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient();

var demoConnStr = builder.Configuration.GetConnectionString("localDB");
builder.Services.AddScoped<IDatabaseHelper>(x => new DatabaseHelper(demoConnStr));
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

//顯示欄位必填標示
//builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
//builder.Services.AddValidatorsFromAssemblyContaining<BookingInputParamaterValidator>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<BookingViewModelValidator>();

builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddEvertrustResponseWrapper();
builder.Services.AddApiVersioning(
    option =>
    {
        option.ReportApiVersions = true;
        option.AssumeDefaultVersionWhenUnspecified = true;
        option.DefaultApiVersion = new ApiVersion(1, 0);
    }
);

//Logging
builder.Services.AddLogHelperFactory();

//Exeptionless
builder.Services.AddExceptionless(
    config =>
    {
        config.SetDefaultMinLogLevel(Exceptionless.Logging.LogLevel.Trace);
    }
);


//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint
        (
            url: "/swagger/v1/swagger.json",
            name: "v1"
        );
    });
    app.UseCoreProfiler(true);
}

app.UseHttpsRedirection();

app.UseCheckTraceId();

//Exeptionless
app.UseExceptionless();

//EvertrustResponseWrapper
app.UseEvertrustResponseWrapper(typeof(Program).Assembly);
app.UseEvertrustExceptionHandling();

app.UseAuthorization();

app.MapControllers();

app.Run();
