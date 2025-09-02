using Microsoft.EntityFrameworkCore;                        // For Entity Framework Core features like UseSqlServer
using Microsoft.AspNetCore.Identity;
using WebAPISandwich.Model;                                 // For SandwichContext DB context

var builder = WebApplication.CreateBuilder(args);           // Initialize the WebApplication builder

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();                          // Add services to the controllers

// Configure the Context's
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbCon")));

builder.Services.AddDbContext<SandwichContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SandwichCon")));


builder.Services.AddEndpointsApiExplorer();                 // Add endpoint API explorer for Swagger/OpenAPI
builder.Services.AddSwaggerGen();                           // Add Swagger generator for API documentation

var app = builder.Build();                                  // Build the WebApplication

app.MapIdentityApi<IdentityUser>();                         // Mapping Identity User

if (app.Environment.IsDevelopment())                        // Configure Middleware
{
    app.UseSwagger();                                       
    app.UseSwaggerUI();                              
}

app.UseHttpsRedirection();                                  // Redirect HTTP requests to HTTPS
app.UseAuthorization();                                     // Enable authorization middleware (if needed)

app.MapControllers();                                       // Map controller routes (e.g., api/Sandwich)

app.Run();                                                  // Run the application