using Microsoft.EntityFrameworkCore;
using IoT_SmartHome.Api.Data;
using IoT_SmartHome.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
// 2. Register the DbContext to use SQLite
builder.Services.AddDbContext<HomeDbContext>(options => 
    options.UseSqlite(connectionString));

// 3. Tell .NET to look for classes that inherit from ControllerBase
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 4. Generate that nice testing webpage.
builder.Services.AddSwaggerGen();

// Add CORS Policy to allow Angular app to access the API
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngular",
		policy => policy.WithOrigins("https://localhost:7016", "http://localhost:5279")
		.AllowAnyMethod()
		.AllowAnyHeader());
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
	{
		var context = scope.ServiceProvider.GetRequiredService<HomeDbContext>();
		// create the Db file if it doesn't exist
		context.Database.EnsureCreated();
		//context.Database.Migrate(); 
		// looks at the Add-Migration scripts to make sure the database structure matches the model

		if (!context.SmartLights.Any())
		{
			context.SmartLights.AddRange(
				new SmartLight { Room = "Kitchen", Brightness = 80, MaxWattage = 60, IsOn = true },
				new SmartLight { Room = "Bedroom", Brightness = 20, MaxWattage = 40, IsOn = false },
				new SmartLight { Room = "Living Room", Brightness = 0, MaxWattage = 100, IsOn = true }
			);
			context.SaveChanges();
		}
	} catch (Exception ex) {
        // Print error to the console instead of crashing the app.
        Console.WriteLine("An error occurred seeding the DB: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();	// This is the .NET 10 default
    app.UseSwagger();	// This generates the JSON 
    app.UseSwaggerUI(); // This creates the interactive website at /swagger
}

app.UseCors("AllowAngular");

// app.UseHttpsRedirection(); // automatically pushes to the secure https://
app.UseAuthorization();		// for logins or keys, this line checks if the user has permission to see the data
app.MapControllers();	// Maps the routes to the Controller methods
app.Run();
