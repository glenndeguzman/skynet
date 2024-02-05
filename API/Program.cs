using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// Add Exception handling middleware at the top
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

// Catch and handle the error; re-direct to errors controller. 
// The {0} placeholder will hold the actual error number that is generated. 
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors("CorsPolicy");  // REfer to settings in ApplicationServicesExtensions.cs

app.UseAuthorization();

app.MapControllers();

// Applies pending migrations to DB. Will create the db is it does not already exist.

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{

    logger.LogError(ex, "An error occured during migration");
}

app.Run();
