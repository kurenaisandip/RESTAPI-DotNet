using Microsoft.Extensions.DependencyInjection.Extensions;
using Movies.Api.Mapping;
using Movies.Application;
using Movies.Application.Database;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

builder.Services.AddApplication();
builder.Services.AddDatabase(config["Data:ConnectionString"]!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

 app.UseAuthorization();

// Add the middleware to the pipeline
// this needs to be before the MapControllers() call as it is squential
 app.UseMiddleware<ValidationMappingMiddleware>();
app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();