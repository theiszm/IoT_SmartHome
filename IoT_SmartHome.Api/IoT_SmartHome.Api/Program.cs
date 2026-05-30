using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// DATABASE & CORE SERVICE CONFIGURATIONS
// ---------------------------------------------------------------------------

// 1. Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "Data Source=IoT_SmartHome.db";

// Add services to the container.
// 2. Register the DbContext to use SQLite
builder.Services.AddDbContext<HomeDbContext>(options => 
    options.UseSqlite(connectionString));

// 3. Enable CORS so your Angular application can communicate with the API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 4. Force JSON responses to output fields in standard frontend camelCase conventions
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// ----------------------------------------------------------------------------
// SWAGGER / OPENAPI SERVICE REGISTRATION
// ----------------------------------------------------------------------------

// 5. Add services to explore endpoints for Swagger documentation
builder.Services.AddEndpointsApiExplorer();
// Generate that nice testing webpage.
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------------------------------------------------------------------
// HTTP REQUEST PIPELINE / MIDDLEWARE (ALL SWAGGER UI IN ONE PLACE)
// ----------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
	// app.MapOpenApi();	// This is the .NET 10 default
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

// Tells .NET to scan the Controllers folder and route URLs to them.
app.MapControllers();

// ----------------------------------------------------------------------------
// DATABASE AUTOMATED GENERATION & SEEDING
// ----------------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
	{
		var context = scope.ServiceProvider.GetRequiredService<HomeDbContext>();
		// create the Db file if it doesn't exist
		context.Database.EnsureCreated();
        // applies any pending migrations to the target database at runtime
        //context.Database.Migrate(); 

        // Seed initial database data if empty
        if (!context.SmartLights.Any())
        {
            context.SmartLights.AddRange(
                new SmartLight { Room = "Kitchen", IsOn = true, Brightness = 80, MaxWattage = 60, IsOnline = true, LastUpdated = DateTime.UtcNow },
                new SmartLight { Room = "Bedroom", IsOn = false, Brightness = 0, MaxWattage = 40, IsOnline = true, LastUpdated = DateTime.UtcNow },
                new SmartLight { Room = "Living Room", IsOn = true, Brightness = 100, MaxWattage = 75, IsOnline = true, LastUpdated = DateTime.UtcNow }
            );
        }

        if (!context.SmartThermostats.Any())
        {
            context.SmartThermostats.AddRange(
                new SmartThermostat { Room = "Living Room", CurrentTemperature = 21.5, TargetTemperature = 22.0, SystemMode = "Heating", IsOnline = true, LastUpdated = DateTime.UtcNow },
                new SmartThermostat { Room = "Bedroom", CurrentTemperature = 19.0, TargetTemperature = 18.5, SystemMode = "Cooling", IsOnline = true, LastUpdated = DateTime.UtcNow }
            );
        }

        if (!context.SmartSpeakers.Any())
        {
            context.SmartSpeakers.AddRange(
                new SmartSpeaker { Room = "Bedroom", Volume = 45, CurrentTrack = "Jars of Clay - Sunny Days", IsMuted = false, IsOnline = true, LastUpdated = DateTime.UtcNow },
                new SmartSpeaker { Room = "Living Room", Volume = 60, CurrentTrack = "Louis Armstrong - What a Wonderful World", IsMuted = false, IsOnline = true, LastUpdated = DateTime.UtcNow }
            );
        }

        if (!context.SmartSecurityCameras.Any())
        {
            context.SmartSecurityCameras.AddRange(
                new SmartSecurityCamera { Room = "Front Porch", IsRecording = true, StorageUsagePercentage = 42, MotionDetected = true, IsOnline = true, LastUpdated = DateTime.UtcNow },
                new SmartSecurityCamera { Room = "Backyard", IsRecording = true, StorageUsagePercentage = 88, MotionDetected = false, IsOnline = true, LastUpdated = DateTime.UtcNow }
            );
        }

        context.SaveChanges();

    } catch (Exception ex) 
    {
        // Print error to the console instead of crashing the app.
        Console.WriteLine("An error occurred seeding the DB: " + ex.Message);
    }
}

app.Run();

// ----------------------------------------------------------------------------
// TYPE DECLARATIONS
// ----------------------------------------------------------------------------
public record DeviceDto(string Type, object Data);