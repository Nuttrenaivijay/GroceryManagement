using GroceryManagement.API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddSingleton<IGroceryService, InMemoryGroceryService>();

// Add controller support
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GroceryManagement API",
        Version = "v1",
        Description = "API for managing grocery items"
    });
});

var app = builder.Build();

// Enable Swagger UI in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroceryManagement API v1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at root
});

// Redirect HTTP to HTTPS (optional if you're not using HTTPS in Docker)
app.UseHttpsRedirection();

// Enable routing and controller mapping
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Start the application
app.Run();
