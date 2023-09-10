using HNGBACKENDTrack;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Set the Configuration to map appsettings
//var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
//builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

var configuration = builder.Configuration
 .SetBasePath(System.IO.Directory.GetCurrentDirectory())
 .AddJsonFile($"appsettings.json", optional: false)
 .AddJsonFile($"appsettings.{port}.json", optional: true)
 .AddEnvironmentVariables()
 .Build();
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
builder.WebHost.UseUrls($"http://*:{port}/");

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
