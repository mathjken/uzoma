using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Uzoma.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Controllers & JSON Formatting
// --------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Forces camelCase for frontend
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // Prevents reference loops
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// --------------------
// Database (PostgreSQL)
// --------------------
string? databaseUrl = builder.Configuration["DATABASE_URL"];
string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    try
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        connectionString =
            $"Host={uri.Host};" +
            $"Port={uri.Port};" +
            $"Database={uri.AbsolutePath.TrimStart('/')};" +
            $"Username={userInfo[0]};" +
            $"Password={userInfo[1]};" +
            $"SSL Mode=Require;Trust Server Certificate=true";

        Console.WriteLine("Using Heroku DATABASE_URL.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error parsing DATABASE_URL: {ex}");
        throw;
    }
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    Console.WriteLine("Using DefaultConnection from appsettings.json.");
}

builder.Services.AddDbContext<UzomaDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// --------------------
// Swagger
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// CORS
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// --------------------
// JWT Authentication
// --------------------
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
{
    Console.WriteLine("WARNING: JWT environment variables are missing!");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? "FallbackSecretKey123!"))
        };
    });

var app = builder.Build();

// --------------------
// Middleware Order
// --------------------
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

// --------------------
// Swagger
// --------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uzoma API V1");
});

// --------------------
// Root endpoint
// --------------------
app.MapGet("/", () => "Uzoma API is running!");

// --------------------
// Map Controllers
// --------------------
app.MapControllers();

// --------------------
// Database migrations & seeding
// --------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UzomaDbContext>();

    try
    {
        context.Database.Migrate();
        Console.WriteLine("Database migration succeeded.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed: {ex}");
    }

    try
    {
        DbInitializer.Initialize(context);
        Console.WriteLine("Database seeding completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database seeding failed: {ex}");
    }
}

// --------------------
// Heroku PORT binding
// --------------------
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");
Console.WriteLine($"Listening on port {port}");

// --------------------
// Run the app
// --------------------
app.Run();
