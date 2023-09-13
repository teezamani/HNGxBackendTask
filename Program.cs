using HNGBACKENDTrack;
using HNGBACKENDTrack.Controllers;
using HNGBACKENDTrack.Model;
using HNGBACKENDTrack.Services;
using HNGBACKENDTrack.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

var configuration = builder.Configuration
 .SetBasePath(System.IO.Directory.GetCurrentDirectory())
 .AddJsonFile($"appsettings.json", optional: false)
 .AddJsonFile($"appsettings.{port}.json", optional: true)
 .AddEnvironmentVariables()
 .Build();
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
builder.WebHost.UseUrls($"http://*:{port}/");

//Register Conssole to use SQL
builder.Services.AddDbContext<HNGxDBContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });

//Register Person service
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();

//Register swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
